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

        public S_0xFEC701_ExEnterWorld() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)0xC7);
            writer.Write((byte)0x01);
            writer.Write(0x846EF15B);
            writer.Write(0x201C0000);
            writer.Write(0);
        }
    }
}
