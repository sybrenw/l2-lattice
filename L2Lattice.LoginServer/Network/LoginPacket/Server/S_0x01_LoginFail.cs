using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x01_LoginFail : SendablePacketBase<LoginClient>
    {
        public static byte Opcode { get; } = 0x01;

        public S_0x01_LoginFail() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {

        }
    }
}
