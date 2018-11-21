using L2Lattice.L2Core;
using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Model;
using L2Lattice.PlayerServer.Network.GamePacket.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace L2Lattice.PlayerServer.Network
{
    public class GameClient : NetworkClient
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<GameClient>();
               
        private static Random Rnd { get; } = new Random();
        
        public byte[] Key => _crypt.Key;

        public int AccountId { get; set; }

        private GameCrypt _crypt;

        public GameClient(Socket socket) : base(socket)
        {
            _crypt = new GameCrypt(GameCrypt.GenerateKey());
        }

        protected override void Initialize()
        {

        }

        protected override void HandlePacket(byte[] raw)
        {
            // Decrypt data block
            _crypt.Decrypt(raw, 0, raw.Length);
            
            ReceivablePacketBase<GameClient> packet = SelectPacket(raw);

            if (packet != null)
            {
                Logger.LogDebug("Received packet {0}", packet.GetType().Name);
                packet.Read(this, raw);
            }
            else
            {
                Logger.LogWarning("Received packet with unknown opcode {0} and length {1}", raw[0], raw.Length);
            }
        }

        public async void SendPacket(SendablePacketBase<GameClient> packet)
        {
            byte[] buffer;
            int length = packet.Write(this, out buffer);
            // Encrypt data block

            byte[] tmp1 = new byte[buffer.Length];
            byte[] tmp2 = new byte[buffer.Length];
            Array.Copy(buffer, tmp1, buffer.Length);

            _crypt.Encrypt(buffer, 2, length - 2);
            //Array.Copy(buffer, tmp2, buffer.Length);
            //_crypt.Decrypt(buffer, 2, length - 2);

            // Copy header
            byte[] header = BitConverter.GetBytes((short)length);
            Array.Copy(header, 0, buffer, 0, 2);
            await SendPacketAsync(buffer, 0, length);
        }

        private ReceivablePacketBase<GameClient> SelectPacket(byte[] raw)
        {
            ReceivablePacketBase<GameClient> packet = null;
            byte opcode = raw[0];
            switch (opcode)
            {
                case C_0x0E_SendProtocolVersion.Opcode:
                    packet = new C_0x0E_SendProtocolVersion();
                    break;
                case C_0x2B_RequestLogin.Opcode:
                    packet = new C_0x2B_RequestLogin();
                    break;
                case C_0x12_RequestGameStart.Opcode:
                    packet = new C_0x12_RequestGameStart();
                    break;
                case C_0x11_RequestEnterWorld.Opcode:
                    packet = new C_0x11_RequestEnterWorld();
                    break;
                case 0xbc:
                case C_0x0F_MoveToLocation.Opcode:
                    packet = new C_0x0F_MoveToLocation();
                    break;
                case 0xD0:
                    packet = SelectPacket_0x0D(raw);
                    break;
            }

            return packet;
        }

        private ReceivablePacketBase<GameClient> SelectPacket_0x0D(byte[] raw)
        {
            ReceivablePacketBase<GameClient> packet = null;
            byte[] op = new byte[2];
            op[0] = raw[2];
            op[1] = raw[1];
            ushort opcode = BitConverter.ToUInt16(op, 0);
            switch(opcode)
            {
                case C_0xD0A600_RequestEx2ndPasswordCheck.SecondaryOpcode:
                    packet = new C_0xD0A600_RequestEx2ndPasswordCheck();
                    break;
                case C_0xD0A700_RequestEx2ndPasswordVerify.SecondaryOpcode:
                    packet = new C_0xD0A700_RequestEx2ndPasswordVerify();
                    break;
                case C_0xD0D100_RequestCashShopBtn.SecondaryOpcode:
                    packet = new C_0xD0D100_RequestCashShopBtn();
                    break;
            }

            return packet;
        }

        internal List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();
            Player player = new Player();
            player.Id = 1;
            player.Name = "Syb";
            player.AccountId = AccountId;
            player.AccountName = "_" + AccountId;
            players.Add(player);
            return players;
        }
    }
}
