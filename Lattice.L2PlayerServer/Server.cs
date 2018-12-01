using Lattice.L2Core;
using Lattice.L2Core.Log;
using Lattice.L2PlayerServer.Service;
using Lattice.L2PlayerServer.World;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lattice.L2PlayerServer
{
    internal class Server
    {
        private static ILogger Logger { get; } = Logging.CreateLogger<Server>();

        public static Network.GameServer GameServer { get; private set; }

        public static Network.LoginServerConnection LoginClient { get; private set; }

        public static CharacterService PlayerService { get; set; }

        public static L2World World { get; set; }

        public static int ServerId = 1;

        public static string LoginIp = "127.0.0.1";

        internal static void Main(string[] args)
        {
            foreach(string arg in args)
            {
                if (arg.StartsWith("--login"))
                {
                    string[] parts = arg.Split("=");
                    if (parts.Length > 1)
                    {
                        Logger.LogInformation("Connecting to loginserver with ip {0}", parts[1]);
                        LoginIp = parts[1];
                    }
                }
            }             


            Start();
        }

        internal static void Start()
        {
            // Set up logging
            Logging.LoggerFactory.AddConsole(LogLevel.Debug, true);

            // Services
            PlayerService = CharacterService.Instance;
            World = L2World.Instance;

            // Connect to loginserver
            Task login = LoginService.Instance.ConnectAsync(LoginIp, 2110);

            // start network server
            Logger.LogInformation("Starting network");
            GameServer = new Network.GameServer();
            Task listen = GameServer.ListenAsync("*", 7777);
            
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
