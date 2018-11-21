using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0x32_ExUserInfo : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0x32;

        public S_0x32_ExUserInfo() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            Player player = Client.GetPlayers()[0];

            // Object id
            writer.Write(1);
            writer.Write(386);
            //?
            writer.Write((short)24);
            writer.Write((byte)255);
            writer.Write((byte)255);
            writer.Write((byte)254);

            writer.Write(0);
            writer.Write((short)(14+player.Name.Length * 2 + 2));
            WriteString2(writer, player.Name);
            writer.Write((byte)0);
            writer.Write((byte)player.Race);
            writer.Write((byte)player.Sex);
            writer.Write(player.VisibleClass);
            writer.Write(player.VisibleClass);
            writer.Write((byte)110);

            string hexBytes = "12005800390050002B002400250021002A000E00A400000034000000520000002600A40000003400000052000000000000000000000000000000000000000000000000000000040000000F00020000000200000001000000010600000000003800280007000000C70100004B00000026000000260000006000000003000000DC000000C6010000140000003000000014000000210000000E00000000000000000000000000120036470200F68D000090F2FFFF0000000012008C0057003200320000000000000000001200000000000000F03F535E834ED941FA3F12000000000000001C406666666666E635400500FE00002000000000000000000000000000000000000000000000000000000000000000160000000000000000000000000000000000140000000F00E02202000000000000000000000C0000000000000000000000040000010A00FFFFFF00A2F9EC00090000000000500000090001000000000000";
            byte[] bytes = new byte[hexBytes.Length / 2];

            for (int i = 0; i < hexBytes.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hexBytes.Substring(i, 2), 16);

            // Unknown (lazy syb)
            writer.Write(bytes);
        }
    }
}
