using Google.Protobuf;
using L2Lattice.L2Core.Network;
using L2Lattice.Protobuf.RegisterPlayerServer;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.PlayerServer.Network
{
    public class LoginServerClient : NetworkClient
    {
        public LoginServerClient()
        {

        }
        
        protected override Task HandlePacket(byte[] raw)
        {
            return Task.CompletedTask;
        }

        protected override void Initialize()
        {
            RegisterPlayerServer msg = new RegisterPlayerServer();
            msg.Id = 1;
            msg.Name = "PlayerServer 1";
            msg.Key = "TestKey";
            byte[] bytes = msg.ToByteArray();
            byte[] header = BitConverter.GetBytes(bytes.Length + 4);
            SendPacketAsync(header, bytes, 0, bytes.Length);
        }
    }
}
