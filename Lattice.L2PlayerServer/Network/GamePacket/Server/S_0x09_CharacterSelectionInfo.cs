using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Model;
using Lattice.L2PlayerServer.Service;
using Lattice.L2Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0x09_CharacterSelectionInfo : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x09;

        public S_0x09_CharacterSelectionInfo() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            IEnumerable<Character> characters = CharacterService.Instance.GetCharacters(Client.AccountId);

            // Characters
            writer.Write(characters.Count());
            // Max characters
            writer.Write(7);
            // ?
            writer.Write(false);
            // Play mode
            writer.Write((byte)2);
            // Korea?
            writer.Write(0);
            // Giftmessage?
            writer.Write(false);
            // ?
            writer.Write(false);
            foreach(Character character in characters)
            {
                WriteString(writer, character.Name);
                writer.Write(character.ObjectId);
                WriteString(writer, "unused");
                writer.Write(character.AccountId);
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
                // Server
                writer.Write(1);
                // X
                writer.Write((int)character.X);
                // Y
                writer.Write((int)character.Y);
                // Z
                writer.Write((int)character.Z);
                // HP / MP
                writer.Write(40000.0);
                writer.Write(40000.0);
                // XP / SP
                writer.Write(100000000L);
                writer.Write(100000000L);
                // Progress
                writer.Write(0.01);
                // Level
                writer.Write(110);
                // Reputation
                writer.Write(99999);
                // PK Count
                writer.Write(0);
                // PvP Count
                writer.Write(0);
                // Unknown
                writer.Write(new byte[9 * 4]);
                /* Equipment */
                // Underwear
                writer.Write(0);
                // Jewels
                writer.Write(new byte[5 * 4]);
                // Head
                writer.Write(35069);
                // Weapon
                writer.Write(36495);
                // Shield
                writer.Write(0);
                // Gloves
                writer.Write(35072);
                // Upper
                writer.Write(35070);
                // Lower
                writer.Write(35071);
                // Boots
                writer.Write(35073);
                // Cloak
                writer.Write(34996);
                // Weapon (2h)
                writer.Write(36495);
                // Hair
                writer.Write(0);
                writer.Write(0);
                // Bracelet
                writer.Write(0);
                writer.Write(0);
                // Talismans
                writer.Write(new byte[6 * 4]);
                // Belt
                writer.Write(0);
                // Unknown
                writer.Write(new byte[4 * 4]);
                // Brooch
                writer.Write(new byte[7 * 4]);
                // Unknown
                writer.Write(new byte[92]);
                // Visible gear
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                // Enchantments
                writer.Write((short)20);
                writer.Write((short)20);
                writer.Write((short)20);
                writer.Write((short)20);
                writer.Write((short)20);
                // Hair / Face
                writer.Write(2);
                writer.Write(0);
                writer.Write(0);
                // Max Hp/Mp
                writer.Write(40000.0);
                writer.Write(40000.0);
                // Time to deletion
                writer.Write(0);
                // Class (old?)
                writer.Write(188);
                // Selected
                writer.Write(1);
                // Weapon enchantment, aug1, aug2
                writer.Write((byte)20);
                writer.Write(0);
                writer.Write(0);
                // Transformation
                writer.Write(0);
                // Pet
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0.0);
                writer.Write(0.0);
                // Vitality
                writer.Write(1400000);
                writer.Write(300);
                // Unknown
                writer.Write(999);
                writer.Write(1);

                writer.Write((byte)1);
                writer.Write((byte)1);
                writer.Write((byte)1);                                
            }
        }
    }
}
