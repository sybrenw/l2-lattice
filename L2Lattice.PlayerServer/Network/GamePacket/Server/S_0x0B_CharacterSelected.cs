using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0x0B_CharacterSelected : SendablePacket<GameClient>
    {
        public const byte Opcode = 0x0B;

        public S_0x0B_CharacterSelected() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Player player = Client.GetPlayers()[0];
            WriteString(writer, player.Name);
            writer.Write(player.Id);
            WriteString(writer, "Wasabi is green");
            // Session id
            writer.Write(1);
            // Clan
            writer.Write(0);
            // Builder level?
            writer.Write(0);
            // Sex
            writer.Write(player.Sex);
            // Race
            writer.Write(player.Race);
            // Class
            writer.Write(player.VisibleClass);
            // Server
            writer.Write(1);

            string hexBytes = "854502006B91000030F2FFFF4408E4EC778C64403963DE366E154A400000000000000000000000000000000001000000000000000000000009080000000000007C00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004809A94C";
            byte[] bytes = new byte[hexBytes.Length / 2];

            for (int i = 0; i < hexBytes.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexBytes.Substring(i, 2), 16);

            // Unknown (lazy syb)
            writer.Write(bytes);

        }
    }
}
