using L2Lattice.L2Core.Crypt;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public class NetworkClient : IDisposable
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<NetworkClient>();

        private static Random rnd = new Random();

        /* Constants */
        const int MINIUM_BUFFER_SIZE = 1024;

        /* Private members */
        private Socket _socket;
        private DuplexPipe _pipe;
        private LoginCrypt _crypt;
        private L2KeyPair _rsaKeyPair;
        private byte[] _blowfish;

        private uint _sessionKey1 = 10336912;
        private uint _sessionKey2 = 684162767;
        private uint _serverSession = 39328;

        // Socket is connected
        private bool _connected = true;


        public NetworkClient(Socket socket)
        {
            _socket = socket;
            _pipe = new DuplexPipe(_socket);
            _rsaKeyPair = new L2KeyPair();

            _blowfish = new byte[16];
            for (int i = 0; i < 16; i++)
                _blowfish[i] = (byte) rnd.Next(0, 255);
            _crypt = new LoginCrypt(_blowfish);
        }

        public NetworkClient(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessAsync()
        {
            Task init = SendInit();
            Task process = _pipe.ProcessAsync();
            Task reading = ReadAsync(_pipe.Input);
            await Task.WhenAll(process, reading);
        }
        
        private async Task ReadAsync(PipeReader reader)
        {
            while(_connected)
            {
                ReadResult result = await reader.ReadAsync();
                
                Logger.LogInformation("Received packet");

                if (result.IsCanceled || result.IsCompleted)
                    break;

                ReadOnlySequence<byte> buffer = result.Buffer;
                SequencePosition? position = buffer.Start;
                
                if (buffer.Length > 2)
                {
                    var header = buffer.Slice(position.Value, 2);
                    int dataLength = BitConverter.ToUInt16(header.First.Span) - 2;

                    position = buffer.GetPosition(2, position.Value);

                    if (buffer.Length >= 2 + dataLength)
                    {
                        var data = buffer.Slice(position.Value, dataLength);
                        byte[] raw = data.ToArray();
                        await HandlePacket(raw);

                        position = buffer.GetPosition(dataLength, position.Value);
                        reader.AdvanceTo(data.End);
                    }
                    else
                    {
                        reader.AdvanceTo(header.End);
                    }
                }
                else
                {
                    reader.AdvanceTo(buffer.End);
                }

            }

            Logger.LogInformation("Reading finished");
        }

        private async Task HandlePacket(byte[] raw)
        {
            if (!_crypt.Decrypt(raw, 0, raw.Length))
            {
                Logger.LogWarning("Checksum failed for packet");
            }


            uint opcode = raw[0];
            //opcode = BinaryPrimitives.ReverseEndianness(opcode);
            Logger.LogInformation("Received packet with opcode " + opcode + " and length " + raw.Length);

            switch (opcode)
            {
                case 0x00:
                    Logger.LogInformation("Received auth login request");
                    await HandleRequestAuthLogin(raw);
                    break;
                case 0x02:
                    Logger.LogInformation("Received server login request");
                    await HandleRequestServerLogin(raw);
                    break;
                case 0x05:
                    Logger.LogInformation("Received server list request");
                    await HandleRequestServerList(raw);
                    break;
                case 0x07:
                    Logger.LogInformation("Received auth GG");
                    await HandleAuthGameGuard(raw);
                    break;
            }


        }

        private async Task HandleAuthGameGuard(byte[] raw)
        {
            uint session = 0;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
                session = reader.ReadUInt32();
            }

            using (MemoryStream stream = new MemoryStream(new byte[512]))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)0x0b);
                writer.Write(session);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }

        }

        private async Task HandleRequestAuthLogin(byte[] raw)
        {
            byte[] data, data2;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
                data = reader.ReadBytes(128);
                data2 = reader.ReadBytes(128);

            }

            byte[] decrypted = _rsaKeyPair.Decrypt(data);
            byte[] decrypted2 = _rsaKeyPair.Decrypt(data2);

            string user = Encoding.ASCII.GetString(decrypted, 1, decrypted.Length - 1).Trim('\0').ToLower();
            string password = Encoding.ASCII.GetString(decrypted2, 1, decrypted2.Length - 1).Trim('\0').ToLower();

            Logger.LogInformation("User logged in: " + user);

            using (MemoryStream stream = new MemoryStream(new byte[512]))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)0x03);
                writer.Write(_sessionKey1);
                writer.Write(_sessionKey2);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Write(0x000003ea);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Write(0x00);
                writer.Write(new byte[16]);
                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }
        }

        private void DecryptRSA(RSAParameters rsaParams)
        {
            BigInteger n = PrepareBigInteger(rsaParams.Modulus);
            BigInteger e = PrepareBigInteger(rsaParams.D);
        }

        private static BigInteger PrepareBigInteger(byte[] unsignedBigEndian)
        {
            // Leave an extra 0x00 byte so that the sign bit is clear
            byte[] tmp = new byte[unsignedBigEndian.Length + 1];
            Buffer.BlockCopy(unsignedBigEndian, 0, tmp, 1, unsignedBigEndian.Length);
            Array.Reverse(tmp);
            return new BigInteger(tmp);
        }

        private async Task HandleRequestServerList(byte[] raw)
        {
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
                reader.ReadUInt32();
                reader.ReadUInt32();
            }

            using (MemoryStream stream = new MemoryStream(new byte[512]))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)0x04);

                writer.Write((byte)_servers.Count);
                writer.Write((byte)1);

                foreach(ServerDummy server in _servers)
                {
                    writer.Write(server.Id);
                    writer.Write(server.Ip);
                    writer.Write(server.Port);
                    writer.Write(server.AgeLimit);
                    writer.Write(server.PvP);
                    writer.Write(server.CurrentPlayers);
                    writer.Write(server.MaxPlayers);
                    writer.Write(server.Status);
                    writer.Write(server.Brackets);
                    writer.Write(server.ServerType);
                }

                writer.Write((short)164);
                writer.Write((byte)0x01);
                writer.Write((byte)0x04);
                writer.Write((byte)0x15);
                writer.Write(new byte[161]);
                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }
        }

        private async Task HandleRequestServerLogin(byte[] raw)
        {
            byte server;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
                reader.ReadUInt32();
                reader.ReadUInt32();
                server = reader.ReadByte();
            }

            using (MemoryStream stream = new MemoryStream(new byte[512]))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)0x07);

                writer.Write(_serverSession);
                writer.Write(_sessionKey1);
                writer.Write(server);
                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }
        }

        private async Task SendInit()
        {
            Logger.LogInformation("Sending init packet");
            using (MemoryStream stream = new MemoryStream(new byte[512]))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // opcode
                writer.Write((byte)0);
                // session id
                writer.Write((uint)230733);
                // Protocol version
                writer.Write((uint)50721);
                // rsa key
                writer.Write(_rsaKeyPair.ScrambledModulus);
                // GG
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(_blowfish); // BlowFish key
                writer.Write(4);

                // Padding
                writer.Write(0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);


                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }
        }

        private async Task SendPacketAsync(byte[] raw, int offset, int size)
        {
            int length = _crypt.Encrypt(raw, offset, size);

            byte[] header = BitConverter.GetBytes((short) (length + 2));
            await _pipe.Output.WriteAsync(header);
            await _pipe.Output.WriteAsync(new ReadOnlyMemory<byte>(raw, offset, length));
            await _pipe.Output.FlushAsync();            
        }

        public void Disconnect()
        {
            _connected = false;
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }

        private List<ServerDummy> _servers = new List<ServerDummy>()
        {
            new ServerDummy() { Id = 1, Ip = 2130706433, Port = 2000, AgeLimit = 0, PvP = 0x01, CurrentPlayers = 8888, MaxPlayers = 9999, Status = 0x01, ServerType = 0x01, Brackets = 0x01 },
            new ServerDummy() { Id = 2, Ip = 2130706433, Port = 2000, AgeLimit = 15, PvP = 0x00, CurrentPlayers = 7777, MaxPlayers = 9999, Status = 0x01, ServerType = 0x32, Brackets = 0x01 },
            new ServerDummy() { Id = 3, Ip = 2130706433, Port = 2000, AgeLimit = 18, PvP = 0x01, CurrentPlayers = 31, MaxPlayers = 9999, Status = 0x01, ServerType = 0x04, Brackets = 0x00 },
            new ServerDummy() { Id = 4, Ip = 2130706433, Port = 2000, AgeLimit = 0, PvP = 0x01, CurrentPlayers = 0, MaxPlayers = 9999, Status = 0x00, ServerType = 0x01, Brackets = 0x00 }
        };


        private class ServerDummy
        {
            public byte Id { get; set; }
            public uint Ip { get; set; }
            public int Port { get; set; }
            public byte AgeLimit { get; set; }
            public byte PvP { get; set; }
            public short CurrentPlayers { get; set; }
            public short MaxPlayers { get; set; }
            public byte Status { get; set; }
            public int ServerType { get; set; }
            public byte Brackets { get; set; }
        }
    }
}
