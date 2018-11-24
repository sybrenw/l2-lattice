using L2Lattice.L2Core;
using L2Lattice.L2Core.Log;
using L2Lattice.L2Core.Network;
using L2Lattice.LoginServer.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace L2Lattice.LoginServer
{
    internal class Server
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<Server>();

        public static Network.LoginListener LoginListener { get; private set; }
        public static Network.PlayerServerListener ServerListener { get; private set; }

        internal static void Main(string[] args)
        {
            Start();
        }

        internal static void Start()
        {
            // Set up logging
            Logging.LoggerFactory.AddConsole(LogLevel.Debug, true);
            //Logging.LoggerFactory.AddCustomConsole(LogLevel.Debug, true);

            // Listen for player servers
            Logger.LogInformation("Listening for player servers on port 2107");
            ServerListener = new Network.PlayerServerListener();
            Task listen1 = ServerListener.ListenAsync("127.0.0.1", 2107);

            // Finally start network server
            Logger.LogInformation("Listening for clients on port 2106");
            LoginListener = new Network.LoginListener();
            Task listen2 = LoginListener.ListenAsync("127.0.0.1", 2106);

            // Wait for tasks to finish before shutting down
            Task.WaitAll(listen1, listen2);
        }

        internal static void Shutdown()
        {
            Logger.LogInformation("Stopping network");
            LoginListener.Close();
        }
    }
}
