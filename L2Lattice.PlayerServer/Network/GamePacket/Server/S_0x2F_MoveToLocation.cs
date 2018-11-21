using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0x2F_MoveToLocation : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0x2F;

        private int[] _dest;
        private int[] _pos;

        public S_0x2F_MoveToLocation(int[] dest, int[] pos) : base(Opcode)
        {
            _dest = dest;
            _pos = pos;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(1);
            for (int i = 0; i < 3; i++)
                writer.Write(_dest[i]);
            for (int i = 0; i < 3; i++)
                writer.Write(_pos[i]);
        }
    }
}
