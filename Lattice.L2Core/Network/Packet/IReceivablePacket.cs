using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lattice.L2Core.Network.Packet
{
    public interface IReceivablePacket<T> where T : L2Client
    {
        Task ReadAsync(T client, byte[] raw);
    }
}
