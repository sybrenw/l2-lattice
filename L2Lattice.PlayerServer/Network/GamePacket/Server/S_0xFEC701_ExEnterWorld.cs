using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFEC701_ExEnterWorld : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0xFE;
        public const ushort SecondaryOpcode = 0xC701;

        public S_0xFEC701_ExEnterWorld() : base(Opcode, SecondaryOpcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(1542739812);
            writer.Write((byte)0x20);
            writer.Write((byte)0x1C);
            writer.Write((byte)0x00);
            writer.Write(0);
        }
    }
}
