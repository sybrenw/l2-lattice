using Lattice.L2Common.Model;
using Lattice.L2Core;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Model;
using Lattice.L2PlayerServer.Network.GamePacket.Client;
using Lattice.L2PlayerServer.World;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer.Network
{
    public class GameClient : L2Client
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<GameClient>();

        private static Random Rnd { get; } = new Random();

        public byte[] Key => _crypt.Key;

        public int AccountId { get; set; }

        public Character Character
        {
            get { return Player.Character; }
            set { Player.Character = value; }
        }
        public Player Player { get; }

        private GameCrypt _crypt;

        public GameClient(Socket socket) : base(socket)
        {
            Player = new Player() { Client = this };
            L2World.Instance.InsertPlayer(Player);
            _crypt = new GameCrypt(GameCrypt.GenerateKey());
            Character = null;
        }

        protected override Task Initialize()
        {
            return Task.CompletedTask;
        }

        protected override int Encrypt(byte[] raw, int offset, int length)
        {
            // Encrypt data block
            _crypt.Encrypt(raw, offset, length);
            return length;
        }

        protected override void Decrypt(byte[] raw, int offset, int length)
        {
            // Decrypt data block
            _crypt.Decrypt(raw, 0, raw.Length);
        }
        
        protected override async Task ProcessPacket(byte[] raw)
        {
            ReceivablePacket<GameClient> packet = SelectPacket(raw);

            if (packet != null)
            {
                Logger.LogDebug("Received packet {0}", packet.GetType().Name);
                await packet.ReadAsync(this, raw);
            }
            else
            {
                Logger.LogWarning("Received packet with unknown opcode {0} and length {1}", raw[0], raw.Length);
            }
        }

        private ReceivablePacket<GameClient> SelectPacket(byte[] raw)
        {
            ReceivablePacket<GameClient> packet = null;
            byte opcode = raw[0];
            Logger.LogInformation("Opcode: {0}", opcode.ToString("X"));
            switch (opcode)
            {
                case C_0x00_Logout.Opcode:
                    packet = new C_0x00_Logout();
                    break;
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
                case C_0x0F_MoveToLocation.Opcode:
                    packet = new C_0x0F_MoveToLocation();
                    break;
                case C_0x13_RequestNewCharacter.Opcode:
                    packet = new C_0x13_RequestNewCharacter();
                    break;
                case C_0x0C_RequestCharacterCreate.Opcode:
                    packet = new C_0x0C_RequestCharacterCreate();
                    break;
                case C_0x56_RequestAction.Opcode:
                    packet = new C_0x56_RequestAction();
                    break;
                case C_0x49_Say.Opcode:
                    packet = new C_0x49_Say();
                    break;
                case 0xD0:
                    packet = SelectPacket_0x0D(raw);
                    break;
            }

            return packet;
        }

        private ReceivablePacket<GameClient> SelectPacket_0x0D(byte[] raw)
        {
            ReceivablePacket<GameClient> packet = null;
            ushort opcode = BitConverter.ToUInt16(raw, 1);
            Logger.LogInformation("Opcode2: {0}", opcode.ToString("X"));
            switch (opcode)
            {
                case C_0xD0_00A6_RequestEx2ndPasswordCheck.SecondaryOpcode:
                    packet = new C_0xD0_00A6_RequestEx2ndPasswordCheck();
                    break;
                case C_0xD0_00A7_RequestEx2ndPasswordVerify.SecondaryOpcode:
                    packet = new C_0xD0_00A7_RequestEx2ndPasswordVerify();
                    break;
                case C_0xD0_00D1_RequestCashShopBtn.SecondaryOpcode:
                    packet = new C_0xD0_00D1_RequestCashShopBtn();
                    break;
                case C_0xD0_0033_RequestLobby.SecondaryOpcode:
                    packet = new C_0xD0_0033_RequestLobby();
                    break;
                case C_0xD0_00A9_RequestCharacterName.SecondaryOpcode:
                    packet = new C_0xD0_00A9_RequestCharacterName();
                    break;

            }

            return packet;
        }

        public void Broadcast(ISendablePacket packet)
        {
            byte[] buffer;
            int length = packet.Write(this, out buffer);
            L2World.Instance.Broadcast(buffer, length, Character.Position, 10000);
        }
    }
}
