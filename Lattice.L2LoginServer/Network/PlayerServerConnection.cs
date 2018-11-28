using Google.Protobuf;
using Lattice.L2Core.Network;
using Lattice.LoginServer.Service;
using Lattice.Protobuf.Login;
using Lattice.Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.LoginServer.Network
{
    internal class PlayerServerConnection : LatticeConnection
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

        protected override async Task ProcessPacket(short opcode, byte[] raw, int rawOffet, int rawSize)
        {
            IMessage msg;
            switch (opcode)
            {
                case 0x00:
                    msg = ReadMessage<RegisterServer>(raw, rawOffet, rawSize);
                    await RegisterServer(msg as RegisterServer);
                    break;
                case 0x01:
                    msg = ReadMessage<PlayerAuthRequest>(raw, rawOffet, rawSize);
                    await HandlePlayerAuthRequest(msg as PlayerAuthRequest);
                    break;
            }

            // TODO: Remove
            await Task.CompletedTask;
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
