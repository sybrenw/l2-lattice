using Lattice.L2Common.Model;
using Lattice.L2PlayerServer.Network;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lattice.L2PlayerServer.Model
{
    public class Player
    {
        public Character Character { get; set; }
        public GameClient Client { get; set; }
    }
}
