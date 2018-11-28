using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.L2Common.World
{
    public interface IRegion
    {
        int CharacterCount { get; }
        void Broadcast(byte[] raw, Vector3 position, int radius);
    }
}
