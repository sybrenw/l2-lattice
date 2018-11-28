using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lattice.L2Core.Network.Packet
{
    public interface ISendablePacket
    {
        int Write(L2Client client, out byte[] buffer);
    }

    public interface ISendablePacket<T> : ISendablePacket where T: L2Client
    {
        int Write(T client, out byte[] buffer);
    }
}
