﻿using Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x07_PlayOk : SendablePacket<LoginClient>
    {
        public static byte Opcode { get; } = 0x07;

        private byte _server;

        public S_0x07_PlayOk(byte server) : base(Opcode)
        {
            _server = server;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Client.Session.GameAuthKey);
            writer.Write(Client.Session.AccountId);
            writer.Write(_server);
            writer.Write(new byte[2]);
        }
    }
}
