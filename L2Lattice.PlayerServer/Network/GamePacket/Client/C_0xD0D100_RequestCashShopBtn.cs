using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using L2Lattice.PlayerServer.Network.GamePacket.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Client
{
    internal class C_0xD0D100_RequestCashShopBtn : ReceivablePacketBase<GameClient>
    {
        public const byte Opcode = 0xD0;
        public const ushort SecondaryOpcode = 0xD100;

        public C_0xD0D100_RequestCashShopBtn()
        {

        }

        public override void Read(BinaryReader reader)
        {
            Client.SendPacket(new S_0xFE3A01_ExCashShopBtn());
        }
    }
}
