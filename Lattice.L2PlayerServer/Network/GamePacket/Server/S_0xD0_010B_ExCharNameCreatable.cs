using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xD0_010B_ExCharNameCreatable : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        private bool _succes;

        public S_0xD0_010B_ExCharNameCreatable(bool success) : base(Opcode, 0x0B, 0x01)
        {
            _succes = success;
        }

        public override void Write(BinaryWriter writer)
        {
            if (_succes)
                writer.Write(-1);
            else
                writer.Write(2);
        }
    }
}
