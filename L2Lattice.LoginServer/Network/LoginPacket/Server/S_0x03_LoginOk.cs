using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x03_LoginOk : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x03;

        public S_0x03_LoginOk() : base(Opcode)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Client.Session.AccountId);
            writer.Write(Client.Session.LoginAuthKey);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Write(0x000003ea);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Write(0x00);
            writer.Write(new byte[16]);
        }
    }
}
