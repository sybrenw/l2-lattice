using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0xD0_00A9_RequestCharacterName : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0xD0;
        public const ushort SecondaryOpcode = 0x00A9;

        public C_0xD0_00A9_RequestCharacterName()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            string name = ReadString(reader);
            Client.SendPacketAsync(new S_0xD0_010B_ExCharNameCreatable(true));
        }
    }
}
