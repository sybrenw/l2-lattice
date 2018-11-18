using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public abstract class NetworkServer: IDisposable
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<NetworkServer>();

        private Socket _socket;

        /* Currently connected clients */
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

            List<Task> clientTasks = new List<Task>();

            while (_running)
            {
                try
                {
                    // Wait for client connection
                    Socket socket = await _socket.AcceptAsync();

                    // Create new client and start processing
                    NetworkClient client = CreateClient(socket);
                    clientTasks.Add(client.ProcessAsync());

                    // Add client to collection
                    _clients.Add(client);

                    Logger.LogDebug("New client connected: " + socket?.RemoteEndPoint?.ToString());
                }
                catch (SocketException ex)
                {
                    Logger.LogError("Failed to accept socket: {0}", ex);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Failed to create new client: {0}", ex);
                }
            }

            // Cleanup
            _socket.Close();
            _socket.Dispose();
            _socket = null;

            Logger.LogInformation("Waiting for clients to disconnect");
            Task.WaitAll(clientTasks.ToArray(), 10000);
            Logger.LogInformation("Network stopped");
        }

        public void Close()
        {
            _running = false;

            foreach(NetworkClient client in _clients)
            {
                client.Disconnect();
            }
        }

        public void Dispose()
        {
            Close();
            _socket?.Dispose();
        }

        /* Abstract methods */
        protected abstract NetworkClient CreateClient(Socket socket);
    }
}
