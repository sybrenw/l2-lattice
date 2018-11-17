using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public class NetworkServer: IDisposable
    {
        private Socket _socket;

        private ConcurrentBag<NetworkClient> _clients = new ConcurrentBag<NetworkClient>();

        /* Event Handlers */
        public event EventHandler<ClientEventArgs> ClientConnected;
        public event EventHandler<ClientEventArgs> ClientDisconnected;

        private bool _running = true;

        public NetworkServer()
        {

        }
        
        public async Task Listen(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            await Listen(ipAddress, port);
        }

        public async Task Listen(IPAddress ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            _socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endPoint);
            _socket.Listen(1000);
            
            while (_running)
            {
                try
                {
                    Socket socket = await _socket.AcceptAsync();
                    NetworkClient client = new NetworkClient(socket);
                    _clients.Add(client);
                    ClientConnected.Invoke(this, new ClientEventArgs(client));
                }
                catch (SocketException e)
                {

                }
                catch (Exception e)
                {
                    
                }
            }

            _socket.Close();
            _socket.Dispose();
            _socket = null;
        }

        public void Close()
        {
            _running = false;
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}
