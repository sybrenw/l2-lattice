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
    internal class S_0x0B_CharacterSelected : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x0B;

        public S_0x0B_CharacterSelected() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Character character = Client.Character;
            WriteString(writer, character.Name);
            writer.Write((int)character.ObjectId);
            WriteString(writer, "Wasabi is green");
            // Session id
            writer.Write(1);
            // Clan
            writer.Write(0);
            // Builder level?
            writer.Write(0);
            // Sex
            writer.Write((int)character.Sex);
            // Race
            writer.Write((int)character.Race);
            // Class
            writer.Write((int)character.VisibleClassId);
            // Selected
            writer.Write(1);
            // Position
            writer.Write((int)character.X);
            writer.Write((int)character.Y);
            writer.Write((int)character.Z);
            // HP/MP
            writer.Write(10000.0);
            writer.Write(10000.0);
            // SP/XP
            writer.Write(0L);
            writer.Write(0L);
            // Level
            writer.Write((int)character.Level);
            // Reputation/PK count
            writer.Write(0);
            writer.Write(0);
            // Game time
            writer.Write(0);
            writer.Write(0);
            // Class
            writer.Write(character.ActiveClassId);
            // Gameguard
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(new byte[64]);

            // Opcode shuffling seed
            writer.Write(0);

        }
    }
}
