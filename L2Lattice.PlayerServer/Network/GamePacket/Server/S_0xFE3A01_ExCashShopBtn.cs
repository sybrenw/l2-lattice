using L2Lattice.L2Core.Network;
using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE3A01_ExCashShopBtn : SendablePacketBase<GameClient>
    {
        public const byte Opcode = 0xFE;
        public const ushort SecondaryOpcode = 0x3A01;

        public S_0xFE3A01_ExCashShopBtn() : base(Opcode, SecondaryOpcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short)1);
        }
    }
}
