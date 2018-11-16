using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public class NetworkClient
    {
        private Socket _socket;

        public NetworkClient(Socket socket)
        {
            _socket = socket;
            Receive();
        }

        public NetworkClient(string ip, int port)
        {
            
        }

        private void Receive()
        {
            Task.Run(async() =>
            {
                var stream = new NetworkStream(_socket);
                byte[] buffer = new byte[24];
                while(true)
                {
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                }
            });
        }
    }
}
