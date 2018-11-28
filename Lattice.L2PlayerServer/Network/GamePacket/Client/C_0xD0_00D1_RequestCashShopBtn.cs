using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using Lattice.L2PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Client
{
    internal class C_0xD0_00D1_RequestCashShopBtn : ReceivablePacket<GameClient>
    {
        public const byte Opcode = 0xD0;
        public const ushort SecondaryOpcode = 0x00D1;

        public C_0xD0_00D1_RequestCashShopBtn()
        {

        }

        protected override void Read(BinaryReader reader)
        {
            Client.SendPacketAsync(new S_0xFE_013A_ExCashShopBtn());
        }
    }
}
