using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x06_PlayFail : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x06;

        private byte _reason;

        public S_0x06_PlayFail(byte reason) : base(Opcode)
        {
            _reason = reason;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_reason);
        }
    }
}
