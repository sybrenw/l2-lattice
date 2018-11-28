using Google.Protobuf;
using Lattice.L2Core;
using Lattice.L2Core.Network;
using Lattice.Protobuf.Login;
using Lattice.Core.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer.Network
{
    public class LoginServerConnection : LatticeConnection
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

        protected override async Task ProcessPacket(short opcode, byte[] raw, int rawOffet, int rawSize)
        {
            IMessage msg;
            switch (opcode)
            {
                case 0x00:
                    msg = ReadMessage<RegisterResponse>(raw, rawOffet, rawSize);
                    HandleRegistrationResponse(msg as RegisterResponse);
                    break;
                case 0x01:
                    msg = ReadMessage<PlayerAuthResponse>(raw, rawOffet, rawSize);
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
