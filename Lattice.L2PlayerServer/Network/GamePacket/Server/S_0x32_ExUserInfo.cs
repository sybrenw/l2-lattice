using Lattice.L2Common.Model;
using Lattice.L2Common.Model.Stats;
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
            CharStats stats = character.Stats;

            // Object id
            writer.Write(character.ObjectId);
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
            writer.Write(stats[BaseStatType.STR]);
            writer.Write(stats[BaseStatType.DEX]);
            writer.Write(stats[BaseStatType.CON]);
            writer.Write(stats[BaseStatType.INT]);
            writer.Write(stats[BaseStatType.WIT]);
            writer.Write(stats[BaseStatType.MEN]);
            writer.Write(stats[BaseStatType.LUC]);
            writer.Write(stats[BaseStatType.CHA]);

            // Bit 0.4 - Max HP/MP/CP
            writer.Write((short)14);
            writer.Write(stats[StatType.MaxHP]);
            writer.Write(stats[StatType.MaxMP]);
            writer.Write(stats[StatType.MaxCP]);

            // Bit 0.3 - Current HP/MP/CP - SP / XP / Progress
            writer.Write((short)38);
            writer.Write(stats.HP);
            writer.Write(stats.MP);
            writer.Write(stats.CP);
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
            writer.Write(stats[StatType.PAtk]);
            writer.Write(stats[StatType.PAtkSpeed]);
            writer.Write(stats[StatType.PDef]);
            writer.Write(stats[StatType.PEvasion]);
            writer.Write(stats[StatType.PAccuracy]);
            writer.Write(stats[StatType.PCritRate]);
            writer.Write(stats[StatType.MAtk]);
            writer.Write(stats[StatType.CastingSpeed]);
            writer.Write(stats[StatType.MAtkSpeed]);
            writer.Write(stats[StatType.MEvasion]);
            writer.Write(stats[StatType.MDef]);
            writer.Write(stats[StatType.MAccuracy]);
            writer.Write(stats[StatType.MCritRate]);

            // Bit 1.6 - Elemental defence
            writer.Write((short)14);
            for (int i = 0; i < 6; i++)
                writer.Write(stats.DefElements[i]);

            // Bit 1.5 - Location
            writer.Write((short)18);
            writer.Write((int)character.X);
            writer.Write((int)character.Y);
            writer.Write((int)character.Z);
            writer.Write(0);

            // Bit 1.4 - Movement speeds
            writer.Write((short)18);
            writer.Write((short)stats[StatType.RunSpeedSlow]);
            writer.Write((short)stats[StatType.RunSpeed]);
            writer.Write((short)stats[StatType.SwimSpeedSlow]);
            writer.Write((short)stats[StatType.SwimSpeed]);
            writer.Write((short)stats[StatType.MountSpeedSlow]);
            writer.Write((short)stats[StatType.MountSpeed]);
            writer.Write((short)stats[StatType.FlySpeedSlow]);
            writer.Write((short)stats[StatType.FlySpeed]);

            // Bit 1.3 - Animation speed
            writer.Write((short)18);
            writer.Write(stats.RunSpeedMultiplier);
            writer.Write(stats.AttackSpeedMultiplier);

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
