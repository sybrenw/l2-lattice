using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network
{
    public interface INetworkClient
    {
        bool Connected { get; }
        void Disconnect();
    }
}
