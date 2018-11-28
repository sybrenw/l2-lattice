using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_013A_ExCashShopBtn : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;

        public S_0xFE_013A_ExCashShopBtn() : base(Opcode, 0x3A, 0x01)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short)1);
        }
    }
}
