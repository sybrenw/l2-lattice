using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0x40_UserAck : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0x40;

        public S_0x40_UserAck() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(1);
            writer.Write(1);
            writer.Write(1);
        }
    }
}
