using L2Lattice.L2Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace L2Lattice.PlayerServer.Network
{
    public class GameServer : NetworkListener
    {


        public GameServer()
        {

        }

        protected override NetworkClient CreateClient(Socket socket)
        {
            return new GameClient(socket);
        }
    }
}
