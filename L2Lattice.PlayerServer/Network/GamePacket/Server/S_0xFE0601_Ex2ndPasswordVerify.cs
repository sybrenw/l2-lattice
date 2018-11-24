using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE0601_Ex2ndPasswordVerify : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE0601_Ex2ndPasswordVerify() : base(Opcode, 0x06, 0x01)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            // No failure
            writer.Write(0);
            writer.Write(0);
        }
    }
}
