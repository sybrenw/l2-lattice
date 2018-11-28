using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x27_SocialAction : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x27;

        private int _objectId;
        private int _actionId;

        public S_0x27_SocialAction(int objectId, int actionId) : base(Opcode)
        {
            _objectId = objectId;
            _actionId = actionId;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_objectId);
            writer.Write(_actionId);
            writer.Write(0);
        }
    }
}
