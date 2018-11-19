using L2Lattice.L2Core.Network.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.LoginServer.Network.LoginPacket.Server
{
    internal class S_0x07_PlayOk : SendablePacketBase<LoginClient>
    {
        public static byte Opcode { get; } = 0x07;

        private byte _server;
        private int _gameServerSessionId;

        public S_0x07_PlayOk(byte server, int gameServerSessionId) : base(Opcode)
        {
            _server = server;
            _gameServerSessionId = gameServerSessionId;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_gameServerSessionId);
            writer.Write(Client.Session.AccountId);
            writer.Write(_server);
            writer.Write(new byte[2]);
        }
    }
}
