using L2Lattice.L2Core.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace L2Lattice.LoginServer.Network
{
    internal class PlayerServerListener : NetworkListener
    {
        private ConcurrentDictionary<int, PlayerServerConnection> _servers = new ConcurrentDictionary<int, PlayerServerConnection>();
        
        protected override NetworkClient CreateClient(Socket socket)
        {
            return new PlayerServerConnection(socket, this);
        }

        public void RegisterServer(int id, PlayerServerConnection connection)
        {
            _servers[id] = connection;
        }
    }
}
