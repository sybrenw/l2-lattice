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
        public const ushort SecondaryOpcode = 0x7C01;

        public S_0xFE7C01_ExChatEnterWorld() : base(Opcode, 0x7C, 01)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(1);
            writer.Write(1);
            writer.Write(16777343);
            writer.Write(2044);
            writer.Write((short)0);
        }
    }
}
