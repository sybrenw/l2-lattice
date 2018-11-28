using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.Core.Network
{
    public interface IConnection
    {
        bool Connected { get; }

        Task ConnectAsync(string ip, int port);
        Task ProcessAsync();
        void Disconnect();
    }
}
