using L2Lattice.L2Core.Network;
using System;
using System.Threading;

namespace L2Lattice.LoginServer
{
    internal class Server
    {
        public static NetworkServer NetworkServer { get; private set; }


        internal static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            NetworkServer = new NetworkServer();
            NetworkServer.Listen("127.0.0.1", 9876).Wait();
        }

        internal static void Shutdown()
        {
            NetworkServer.Close();
        }
    }
}
