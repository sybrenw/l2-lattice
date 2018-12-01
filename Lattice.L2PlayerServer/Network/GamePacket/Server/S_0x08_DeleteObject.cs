using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x08_DeleteObject : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x08;

        private int _objectId;

        public S_0x08_DeleteObject(int objectId) : base(Opcode)
        {
            _objectId = objectId;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_objectId);
            writer.Write((byte)0);
        }
    }
}
