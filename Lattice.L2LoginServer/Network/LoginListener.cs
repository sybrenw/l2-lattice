using Lattice.L2Core.Network;
using Lattice.LoginServer.Service;
using Lattice.Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Lattice.LoginServer.Network
{
    public class LoginListener : NetworkListener
    {        
        internal LoginListener()
        {

        }

        protected override NetworkConnection CreateConnection(Socket socket)
        {
            return new LoginClient(socket);
        }
    }
}
