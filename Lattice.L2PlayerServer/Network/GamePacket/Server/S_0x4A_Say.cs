using Lattice.L2Common.Enum;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x4A_Say : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x4A;

        private int _objectId;
        private ChatType _type;
        private string _sender;
        private string _message;

        public S_0x4A_Say(int objectId, ChatType type, string sender, string message) : base(Opcode)
        {
            _objectId = objectId;
            _type = type;
            _sender = sender;
            _message = message;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_objectId);
            writer.Write((int)_type);
            WriteString(writer, _sender);
            writer.Write(0xFFFF);
            WriteString(writer, _message);
        }
    }
}
