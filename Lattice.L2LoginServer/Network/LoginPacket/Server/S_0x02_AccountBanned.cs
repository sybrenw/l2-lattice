﻿using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x02_AccountBanned : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x02;

        private uint _reason;

        public S_0x02_AccountBanned(uint reason) : base(Opcode)
        {
            _reason = reason;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_reason);
        }
    }
}
