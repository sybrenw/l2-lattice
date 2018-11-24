using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace L2Lattice.L2Core.Network.Packet
{
    public interface IReceivablePacket<T> where T : INetworkClient
    {
        Task ReadAsync(T client, byte[] raw);
    }
}
