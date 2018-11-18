using L2Lattice.L2Core.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace L2Lattice.LoginServer.Network
{
    public class LoginServer : NetworkServer
    {
        public LoginServer()
        {

        }

        protected override NetworkClient CreateClient(Socket socket)
        {
            return new LoginClient(socket);
        }
    }
}
