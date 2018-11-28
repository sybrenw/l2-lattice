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
            // Server
            writer.Write(1);

            string hexBytes = "854502006B91000030F2FFFF4408E4EC778C64403963DE366E154A400000000000000000000000000000000001000000000000000000000009080000000000007C000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            byte[] bytes = new byte[hexBytes.Length / 2];

            for (int i = 0; i < hexBytes.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexBytes.Substring(i, 2), 16);

            // Unknown (lazy syb)
            writer.Write(bytes);

        }
    }
}
