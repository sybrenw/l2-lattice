using L2Lattice.L2Core;
using L2Lattice.L2Core.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace L2Lattice.LoginServer
{
    internal class Server
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<Server>();

        public static NetworkServer NetworkServer { get; private set; }


        internal static void Main(string[] args)
        {
            Start();
        }

        internal static void Start()
        {
            // Set up logging
            Logging.LoggerFactory.AddConsole(LogLevel.Debug, true);

            // Finally start network server
            Logger.LogInformation("Starting network");
            NetworkServer = new NetworkServer();
            NetworkServer.Listen("127.0.0.1", 2106).Wait();
        }

        internal static void Shutdown()
        {
            Logger.LogInformation("Stopping network");
            NetworkServer.Close();
        }
    }
}
