using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x01_LoginFail : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x01;

        public S_0x01_LoginFail() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)0x02);
        }
    }
}
