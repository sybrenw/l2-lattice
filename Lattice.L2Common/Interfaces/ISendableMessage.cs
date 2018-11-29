using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Common.Interfaces
{
    public interface ISendableMessage
    {
        byte[] Bytes { get; }
        int Size { get; }
    }
}
