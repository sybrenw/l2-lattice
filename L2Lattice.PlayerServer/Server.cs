using L2Lattice.L2Core;
using L2Lattice.L2Core.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace L2Lattice.PlayerServer
{
    internal class Server
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<Server>();

        public static Network.GameServer GameServer { get; private set; }

        public static Network.LoginServerClient LoginClient { get; private set; }

        internal static void Main(string[] args)
        {
            Start();
        }

        internal static void Start()
        {
            // Set up logging
            Logging.LoggerFactory.AddConsole(LogLevel.Debug, true);

            // Register server
            Logger.LogInformation("Connecting to loginserver");
            LoginClient = new Network.LoginServerClient();
            Task login = LoginClient.ConnectAsync("127.0.0.1", 2107);

            // Finally start network server
            Logger.LogInformation("Starting network");
            GameServer = new Network.GameServer();
            Task listen = GameServer.ListenAsync("127.0.0.1", 7777);

            // Wait till all tasks finished
            Task.WaitAll(login, listen);
        }

        internal static void Shutdown()
        {
            Logger.LogInformation("Stopping network");
            GameServer.Close();
        }
    }
}
