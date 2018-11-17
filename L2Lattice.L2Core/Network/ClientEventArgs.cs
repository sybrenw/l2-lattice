using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.L2Core.Network
{
    public class ClientEventArgs : EventArgs
    {
        public NetworkClient Client { get; }

        public ClientEventArgs(NetworkClient client)
        {
            Client = client;
        }
    }
}
