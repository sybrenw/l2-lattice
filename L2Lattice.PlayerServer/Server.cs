using L2Lattice.L2Core;
using Microsoft.Extensions.Logging;
using System;

namespace L2Lattice.PlayerServer
{
    internal class Server
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<Server>();

        public static Network.LoginServer LoginServer { get; private set; }


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
            LoginServer = new Network.LoginServer();
            LoginServer.Listen("127.0.0.1", 2106).Wait();
        }

        internal static void Shutdown()
        {
            Logger.LogInformation("Stopping network");
            LoginServer.Close();
        }
    }
}
