using Lattice.L2Common.Model;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_00EA_ExSubClassInfo : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE_00EA_ExSubClassInfo() : base(Opcode,0xEA, 0x00)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Character character = Client.Character;
            // Current class
            writer.Write(character.VisibleClassId);
            // Race
            writer.Write((int)character.Race);
            //Available classes
            writer.Write(1);

            // Loop
            // Slot
            writer.Write(0);
            // Class
            writer.Write(character.VisibleClassId);
            // Level
            writer.Write(110);
            // Sub?
            writer.Write((byte)0);

        }
    }
}
