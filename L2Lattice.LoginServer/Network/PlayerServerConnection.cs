using Google.Protobuf;
using L2Lattice.L2Core.Network;
using L2Lattice.LoginServer.Service;
using L2Lattice.Protobuf.Login;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer.Network
{
    internal class PlayerServerConnection : LatticeClient
    {
        public int ServerId { get; private set; }
        public int ServerName { get; private set; }

        private PlayerServerListener _listener;

        internal PlayerServerConnection(Socket socket, PlayerServerListener listener) : base(socket)
        {

        }

        protected override Task Initialize()
        {
            return Task.CompletedTask;
        }

        protected override async Task HandlePacket(byte[] raw)
        {
            // Read opcode
            byte opcode = raw[0];

            // Data length/offset
            int offset = 2;
            int length = raw.Length - 2;

            IMessage msg;
            switch(opcode)
            {
                case 0x00:
                    msg = ReadMessage<RegisterServer>(raw);
                    await RegisterServer(msg as RegisterServer);
                    break;
                case 0x01:
                    msg = ReadMessage<PlayerAuthRequest>(raw);
                    await HandlePlayerAuthRequest(msg as PlayerAuthRequest);
                    break;
            }
        }

        private async Task RegisterServer(RegisterServer msg)
        {
            RegisterResponse response = new RegisterResponse();

            ServerId = msg.Id;
            ServerName = msg.Id;

            if (ServerService.Instance.RegisterServer(ServerId, this))
            {
                response.Id = msg.Id;
                response.Result = 1;
            }
            else
            {
                response.Id =0;
                response.Result = 0;
            }

            await SendPacketAsync(0x00, response);
        }

        private async Task HandlePlayerAuthRequest(PlayerAuthRequest msg)
        {            
            // Create response
            PlayerAuthResponse response = new PlayerAuthResponse();
            response.AccountId = msg.AccountId;

            if (LoginService.Instance.VerifySession(msg.AccountId, msg.LoginAuthKey, msg.GameAuthKey))
                response.Result = 1;
            else
                response.Result = 0;

            await SendPacketAsync(0x01, response);
        }
    }
}
