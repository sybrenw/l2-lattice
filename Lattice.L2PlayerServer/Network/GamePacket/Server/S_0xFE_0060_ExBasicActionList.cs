using Lattice.L2Core.Network;
using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2PlayerServer.Network.GamePacket.Server
{
    internal class S_0xFE_0060_ExBasicActionList : SendablePacket<GameClient>
    {
        public const byte Opcode = 0xFE;


        public S_0xFE_0060_ExBasicActionList() : base(Opcode, 0x60, 0x00)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            int count1 = 93; // 0 -> 92
            int count2 = 1157; // 1000 -> 1156
            int count3 = 16; // 5000 -> 5015

            writer.Write(count1 + count2 + count3);

            for (int i = 0; i < count1; i++)
                writer.Write(i);
            for (int i = 1000; i < 1000 + count2; i++)
                writer.Write(i);
            for (int i = 5000; i < 5000 + count3; i++)
                writer.Write(i);
        }

        
    }
}
