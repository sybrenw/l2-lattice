using L2Lattice.L2Core.Crypt;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Sockets;
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
        private RSA _rsa;
        private RSAParameters _privateKey;
        private byte[] _publicKey;
        private byte[] _blowfish;

        private uint _sessionKey1 = 10336912;
        private uint _sessionKey2 = 684162767;

        // Socket is connected
        private bool _connected = true;


        public NetworkClient(Socket socket)
        {
            _socket = socket;
            _pipe = new DuplexPipe(_socket);
            _rsa = RSA.Create();
            _rsa.KeySize = 1024;
            _privateKey = _rsa.ExportParameters(true);
            _publicKey = _privateKey.Modulus;

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
                    }
                }

                reader.AdvanceTo(buffer.Start, buffer.End);
            }
        }

        private async Task HandlePacket(byte[] raw)
        {
            if (!_crypt.Decrypt(raw, 0, raw.Length))
            {
                Logger.LogWarning("Checksum failed for packet");
            }


            uint opcode = BitConverter.ToUInt16(raw, 0);
            //opcode = BinaryPrimitives.ReverseEndianness(opcode);
            Logger.LogInformation("Received packet with opcode " + opcode + " and length " + raw.Length);

            switch (opcode)
            {
                case 0x00:
                    await HandleRequestAuthLogin(raw);
                    break;
                case 0x02:
                    await HandleRequestServerLogin(raw);
                    break;
                case 0x05:
                    await HandleRequestServerList(raw);
                    break;
                case 0x07:
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
            byte[] data;
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
                data = reader.ReadBytes(raw.Length - 1);
            }
            byte[] decrypted = _rsa.DecryptValue(data);
            
            string user = BitConverter.ToString(decrypted, 0x5E, 14).Trim().ToLower();
            string password = BitConverter.ToString(decrypted, 0x6C, 16).Trim();
            int ncotp = decrypted[0x7c];
            ncotp |= decrypted[0x7d] << 8;
            ncotp |= decrypted[0x7e] << 16;
            ncotp |= decrypted[0x7f] << 24;

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
                    writer.Write(server.ServerType);
                    writer.Write(server.Brackets);
                }

                writer.Write(0L);
                writer.Write((byte)0);
                writer.Flush();
                await SendPacketAsync(stream.ToArray(), 0, (int)stream.Position);
            }
        }

        private async Task HandleRequestServerLogin(byte[] raw)
        {
            using (MemoryStream stream = new MemoryStream(raw))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                reader.ReadByte();
            }
        }

        private async Task SendInit()
        {
            Logger.LogInformation("Sending init packet");
            byte[] packet = null;
            int length = 0;
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // opcode
                writer.Write((byte)0);
                // session id
                writer.Write((uint)230733);
                // Protocol version
                writer.Write((uint)50721);
                // rsa key
                writer.Write(_publicKey);
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
                length = (int)stream.Length;
                // Buffer
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Flush();
                packet = stream.ToArray();
            }

            if (packet != null)
                await SendPacketAsync(packet, 0, length);
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
            new ServerDummy() { Id = 1, Ip = 2130706433, Port = 2000, AgeLimit = 0, PvP = 0x01, CurrentPlayers = 12424, MaxPlayers = 99999, Status = 0x01, ServerType = 0x01, Brackets = 0x01 },
            new ServerDummy() { Id = 2, Ip = 2130706433, Port = 2000, AgeLimit = 15, PvP = 0x00, CurrentPlayers = 1337, MaxPlayers = 99999, Status = 0x01, ServerType = 0x32, Brackets = 0x01 },
            new ServerDummy() { Id = 3, Ip = 2130706433, Port = 2000, AgeLimit = 18, PvP = 0x01, CurrentPlayers = 31, MaxPlayers = 99999, Status = 0x01, ServerType = 0x04, Brackets = 0x00 },
            new ServerDummy() { Id = 4, Ip = 2130706433, Port = 2000, AgeLimit = 0, PvP = 0x01, CurrentPlayers = 0, MaxPlayers = 99999, Status = 0x00, ServerType = 0x01, Brackets = 0x00 }
        };


        private class ServerDummy
        {
            public byte Id { get; set; }
            public uint Ip { get; set; }
            public int Port { get; set; }
            public byte AgeLimit { get; set; }
            public byte PvP { get; set; }
            public long CurrentPlayers { get; set; }
            public long MaxPlayers { get; set; }
            public byte Status { get; set; }
            public int ServerType { get; set; }
            public byte Brackets { get; set; }
        }
    }
}
