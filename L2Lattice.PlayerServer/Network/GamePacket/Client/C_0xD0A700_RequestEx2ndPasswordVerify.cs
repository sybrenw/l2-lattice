using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Client
{
    internal class C_0xD0A700_RequestEx2ndPasswordVerify : ReceivablePacketBase<GameClient>
    {
        public const byte Opcode = 0xD0;
        public const ushort SecondaryOpcode = 0x0A700;

        public C_0xD0A700_RequestEx2ndPasswordVerify()
        {

        }

        public override void Read(BinaryReader reader)
        {
            reader.ReadByte();
            reader.ReadByte();

            string key = ReadString(reader);

            Client.SendPacket(new S_0xFE0601_Ex2ndPasswordVerify());
        }
    }
}
