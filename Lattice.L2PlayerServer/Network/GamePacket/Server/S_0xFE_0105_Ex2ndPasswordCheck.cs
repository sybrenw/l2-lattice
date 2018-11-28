using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_0105_Ex2ndPasswordCheck : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE_0105_Ex2ndPasswordCheck() : base(Opcode, 0x05, 0x01)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(1);
            writer.Write(0);
        }
    }
}
