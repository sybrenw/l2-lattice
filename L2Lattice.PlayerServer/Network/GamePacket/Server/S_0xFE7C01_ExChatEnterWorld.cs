using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE7C01_ExChatEnterWorld : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE7C01_ExChatEnterWorld() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)0x7C);
            writer.Write((byte)0x01);
            writer.Write(1);
            writer.Write(1);
            writer.Write(16777343);
            writer.Write(2044);
            writer.Write((short)0);
        }
    }
}
