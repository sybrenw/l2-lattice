using L2Lattice.L2Core.Network;
using L2Lattice.Protobuf.RegisterPlayerServer;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network
{
    internal class PlayerServerConnection : NetworkClient
    {
        internal PlayerServerConnection(Socket socket) : base(socket)
        {

        }

        protected override void Initialize()
        {

        }

        protected override Task HandlePacket(byte[] raw)
        {
            byte opcode = raw[0];

            switch(opcode)
            {
                case 0x00:
                    HandleRegisterPS(raw);
                    break;
            }


            return Task.CompletedTask;
        }

        private void HandleRegisterPS(byte[] raw)
        {
            RegisterPlayerServer msg = RegisterPlayerServer.Parser.ParseFrom(raw, 2, raw.Length - 2);
        }
    }
}
