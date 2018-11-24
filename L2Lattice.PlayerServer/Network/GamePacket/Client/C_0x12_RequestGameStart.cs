using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Client
{
    internal class C_0x12_RequestGameStart : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0x12;

        public C_0x12_RequestGameStart()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            int character = reader.ReadInt32();
            reader.ReadInt16();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();

            Client.SendPacket(new S_0x0B_CharacterSelected());
            Client.SendPacket(new S_0xFEEA00_ExSubClassInfo());
        }
    }
}
