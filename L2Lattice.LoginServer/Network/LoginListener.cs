﻿using L2Lattice.L2Core.Network;
using L2Lattice.LoginServer.Service;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace L2Lattice.LoginServer.Network
{
    public class LoginListener : NetworkListener
    {
        internal LoginService LoginService { get; }
        
        internal LoginListener()
        {
            // Start services
            LoginService = new LoginService();
        }

        protected override NetworkClient CreateClient(Socket socket)
        {
            return new LoginClient(socket, LoginService);
        }
    }
}