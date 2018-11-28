using Lattice.L2Core.Network;
using Lattice.Core.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Lattice.LoginServer.Network
{
    internal class PlayerServerListener : NetworkListener
    {
        private ConcurrentDictionary<int, PlayerServerConnection> _servers = new ConcurrentDictionary<int, PlayerServerConnection>();
        
        protected override NetworkConnection CreateConnection(Socket socket)
        {
            return new PlayerServerConnection(socket, this);
        }

        public void RegisterServer(int id, PlayerServerConnection connection)
        {
            _servers[id] = connection;
        }
    }
}
