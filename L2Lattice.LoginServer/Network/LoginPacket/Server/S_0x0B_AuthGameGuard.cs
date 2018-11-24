using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x0B_AuthGameGuard : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x0B;

        public S_0x0B_AuthGameGuard() : base(Opcode)
        {

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Client.Session.Id);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Flush();
        }
    }
}
