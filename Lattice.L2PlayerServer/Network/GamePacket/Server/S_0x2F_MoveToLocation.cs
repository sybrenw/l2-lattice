using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x2F_MoveToLocation : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x2F;

        private int _objectId;
        private int[] _dest;
        private int[] _pos;

        public S_0x2F_MoveToLocation(int objectId, int[] dest, int[] pos) : base(Opcode)
        {
            _objectId = objectId;
            _dest = dest;
            _pos = pos;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_objectId);
            for (int i = 0; i < 3; i++)
                writer.Write(_dest[i]);
            for (int i = 0; i < 3; i++)
                writer.Write(_pos[i]);
        }
    }
}
