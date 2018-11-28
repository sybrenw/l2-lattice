using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_01C7_ExEnterWorld : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE_01C7_ExEnterWorld() : base(0xFE, 0xC7, 0x01)
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
