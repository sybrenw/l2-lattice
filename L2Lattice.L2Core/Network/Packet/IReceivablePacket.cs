using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L2Lattice.L2Core.Network.Packet
{
    public interface IReceivablePacket
    {
        void Read(BinaryReader reader);
    }
}
