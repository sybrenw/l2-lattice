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
    internal class S_0x32_ExUserInfo : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x32;

        public S_0x32_ExUserInfo() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Character character = Client.Character;

            // Object id
            writer.Write(1);
            // Dynamic content size
            long lengthPos = writer.BaseStream.Position;
            writer.Write(0);
            long startPos = writer.BaseStream.Position;
            // Struct type
            writer.Write((short)24);
            // Bitmasks
            writer.Write((byte)255);
            writer.Write((byte)255);
            writer.Write((byte)254);

            // Bit 0.7 - User role
            writer.Write(0);

            // Bit 0.6 - Name and appearance
            writer.Write((short)(14 + character.Name.Length * 2 + 2));
            WriteString2(writer, character.Name);
            writer.Write((byte)0);
            writer.Write((byte)character.Race);
            writer.Write((byte)character.Sex);
            writer.Write(character.VisibleClassId);
            writer.Write(character.ActiveClassId);
            writer.Write((byte)character.Level);


            // Bit 0.5 - Base stats
            writer.Write((short)18);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);
            writer.Write((short)80);

            // Bit 0.4 - Max HP/MP/CP
            writer.Write((short)14);
            writer.Write(100000);
            writer.Write(100000);
            writer.Write(100000);

            // Bit 0.3 - Current HP/MP/CP - SP / XP / Progress
            writer.Write((short)38);
            writer.Write(100000);
            writer.Write(100000);
            writer.Write(100000);
            writer.Write(0L);
            writer.Write(0L);
            writer.Write(0.0);

            // Bit 0.2 - Enchant glows
            writer.Write((short)4);
            writer.Write((byte)20);
            writer.Write((byte)20);

            // Bit 0.1 - Facial features
            writer.Write((short)15);
            writer.Write(3);
            writer.Write(1);
            writer.Write(2);
            writer.Write((byte)1);

            // Bit 0.0 - Personal store
            writer.Write((short)6);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);

            // Bit 1.7 - Character stats
            writer.Write((short)56);
            writer.Write((short)40);
            writer.Write(100000);
            writer.Write(2000);
            writer.Write(200000);
            writer.Write(200);
            writer.Write(200);
            writer.Write(900);
            writer.Write(100000);
            writer.Write(3000);
            writer.Write(2000);
            writer.Write(200);
            writer.Write(200000);
            writer.Write(200);
            writer.Write(500);

            // Bit 1.6 - Elemental defence
            writer.Write((short)14);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);

            // Bit 1.5 - Location
            writer.Write((short)18);
            writer.Write((int)character.X);
            writer.Write((int)character.Y);
            writer.Write((int)character.Z);
            writer.Write(0);

            // Bit 1.4 - Movement speeds
            writer.Write((short)18);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);
            writer.Write((short)1000);

            // Bit 1.3 - Animation speed
            writer.Write((short)18);
            writer.Write(2.0);
            writer.Write(2.0);

            // Bit 1.2 - Collision radius/size
            writer.Write((short)18);
            writer.Write(8.0);
            writer.Write(24.0);

            // Bit 1.1 - Elemental attack
            writer.Write((short)5);
            writer.Write((byte)1);
            writer.Write((short)2000);

            // Bit 1.0 - Clan info / LFP
            writer.Write((short)32);
            WriteString(writer, "");
            writer.Write((short)0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((byte)0);

            // Bit 2.7 - Pvp / PK / hero stats
            writer.Write((short)22);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write((byte)1);
            writer.Write((byte)1);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((short)0);
            writer.Write((short)0);

            // Bit 2.6 - Vitality / raid points
            writer.Write((short)15);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write(0);

            // Bit 2.5 - Talismans / slots
            writer.Write((short)12);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write(0);

            // Bit 2.4 - Movement type
            writer.Write((short)4);
            writer.Write((byte)0);
            writer.Write((byte)1);

            // Bit 2.3 - Name/title color
            writer.Write((short)10);
            writer.Write(16777215);
            writer.Write(15530402);

            // Bit 2.2 - Mount / inventory slots
            writer.Write((short)9);
            writer.Write(0);
            writer.Write((short)80);
            writer.Write((byte)0);

            // Bit 2.1 - Unknown
            writer.Write((short)9);
            writer.Write((byte)1);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write((byte)0);

            // Bit 2.0 - Unused

            // Fill out data length
            long endPos = writer.BaseStream.Position;
            writer.BaseStream.Position = lengthPos;
            writer.Write((int)(endPos - startPos));
            writer.BaseStream.Position = endPos;
        }
    }
}
