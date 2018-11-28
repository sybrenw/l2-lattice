using Lattice.L2Core.Network;
using Lattice.Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Lattice.L2PlayerServer.Network
{
    public class GameServer : NetworkListener
    {


        public GameServer()
        {

        }

        protected override NetworkConnection CreateConnection(Socket socket)
        {
            return new GameClient(socket);
        }
    }
}
