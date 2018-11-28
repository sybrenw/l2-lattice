using Lattice.L2Common.Model;
using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x31_CharInfo : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x31;

        private Character _character;

        public S_0x31_CharInfo(Character character) : base(Opcode)
        {
            _character = character;
        }

        public override void Write(BinaryWriter writer)
        {
            // Unknown
            writer.Write((byte)0x00);
            // Position
            writer.Write((int)_character.Position.X);
            writer.Write((int)_character.Position.Y);
            writer.Write((int)_character.Position.Z);
            // Vehicle OID
            writer.Write(0);
            writer.Write(_character.ObjectId);
            WriteString(writer, _character.Name);
            writer.Write((short)_character.Race);
            writer.Write((byte)_character.Sex);
            writer.Write(_character.VisibleClassId);
            // Equipment
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            //Hair
            writer.Write(0);
            writer.Write(0);
            // Weapon aug
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            // Armor glow
            writer.Write((byte)0);
            // Weapon armor appearance
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            // In pvp?
            writer.Write((byte)0);
            // Reputation
            writer.Write(0);
            // Casting and attack speed
            writer.Write(1000);
            writer.Write(1000);
            // Walking speed
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write((short)100);
            writer.Write(1.25);
            writer.Write(2.42);
            // Collision radius
            writer.Write(6.5);
            writer.Write(19.5);
            // Hair style
            writer.Write((int)_character.HairStyle);
            writer.Write((int)_character.HairColor);
            writer.Write((int)_character.Face);
            // Title
            WriteString(writer, "");
            // Clan
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            // Movement mode etc
            writer.Write((byte)1);
            writer.Write((byte)1);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            // Cubics
            writer.Write((short)0);

            // LFP
            writer.Write((byte)0);
            // Environment
            writer.Write((byte)0);
            // Eval score
            writer.Write((short)0);
            // Mount
            writer.Write(0);
            // Class
            writer.Write(_character.ActiveClassId);
            writer.Write(0);
            // Enchant glow weapon
            writer.Write((byte)0);
            // Duel etc
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write((byte)0);
            // Fishing
            writer.Write((byte)0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            // Name color
            writer.Write(16777215);
            // Heading
            writer.Write(14206);
            // Social class
            writer.Write((byte)0);
            writer.Write((short)0);
            // Title color
            writer.Write(15530402);
            // Cursed weapon level
            writer.Write((byte)0);
            // Repu etc
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write((byte)1);
            // HP etc
            writer.Write(20000);
            writer.Write(20000);
            writer.Write(20000);
            writer.Write(20000);
            writer.Write(20000);
            // Special effect
            writer.Write((byte)0);
            writer.Write(0);

            writer.Write((byte)0);
            writer.Write((byte)1);
            writer.Write((byte)0);
        }
    }
}
