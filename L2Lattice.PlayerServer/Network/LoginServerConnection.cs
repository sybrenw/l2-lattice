using Google.Protobuf;
using L2Lattice.L2Core;
using L2Lattice.L2Core.Network;
using L2Lattice.Protobuf.Login;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L2Lattice.PlayerServer.Network
{
    public class LoginServerConnection : LatticeClient
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<LoginServerConnection>();

        public int ServerId { get; set; }

        private ConcurrentDictionary<int, AuthRequest> _authRequests = new ConcurrentDictionary<int, AuthRequest>();

        public LoginServerConnection(int serverId)
        {
            ServerId = serverId;
        }

        protected override async Task Initialize()
        {
            RegisterServer msg = new RegisterServer
            {
                Id = ServerId,
                Name = "PlayerServer " + ServerId,
                Key = "TestKey"
            };
            await SendPacketAsync(0x00, msg);
        }

        protected override async Task HandlePacket(byte[] raw)
        {
            // Read opcode
            byte opcode = raw[0];

            // Data length/offset
            int offset = 2;
            int length = raw.Length - 2;

            IMessage msg;
            switch (opcode)
            {
                case 0x00:
                    msg = ReadMessage<RegisterResponse>(raw);
                    HandleRegistrationResponse(msg as RegisterResponse);
                    break;
                case 0x01:
                    msg = ReadMessage<PlayerAuthResponse>(raw);
                    HandlePlayerAuthResponse(msg as PlayerAuthResponse);
                    break;
            }

            // TODO: Remove
            await Task.CompletedTask;
        }

        private void HandleRegistrationResponse(RegisterResponse msg)
        {
            if (msg.Result > 0)
            {
                Logger.LogInformation("Succesfully registered as server " + msg.Id);
            }
            else
            {
                Logger.LogInformation("Failed to register");
            }
        }

        private void HandlePlayerAuthResponse(PlayerAuthResponse msg)
        {
            if (_authRequests.TryGetValue(msg.AccountId, out AuthRequest request))
            {
                Logger.LogInformation("Player auth result received: " + msg.Result);
                request.Success = (msg.Result > 0);
                request.Signal.Release();
            }
        }

        public async Task<bool> GetPlayerAuthedAsync(int accountId, int loginAuthKey, int gameAuthKey)
        {
            AuthRequest request = new AuthRequest();

            if (_authRequests.TryAdd(accountId, request))
            {
                PlayerAuthRequest msg = new PlayerAuthRequest()
                {
                    AccountId = accountId,
                    LoginAuthKey = loginAuthKey,
                    GameAuthKey = gameAuthKey,
                    Server = ServerId
                };
                await SendPacketAsync(0x01, msg);
                await request.Signal.WaitAsync();
            }

            return request.Success;
        }

        private class AuthRequest
        {
            public SemaphoreSlim Signal { get; } = new SemaphoreSlim(0, 1);
            public bool Success { get; set; }
        }
    }
}
