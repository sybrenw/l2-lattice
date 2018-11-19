using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.PlayerServer.Model
{
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public int AccountId { get; set; }
        public int ClanId { get; }

        public int Sex { get; } = 1;
        public int Race { get; } = 6;
        public int VisibleClass { get; } = 182;
        public int Server { get; } = 1;

        public int X { get; } = 87458;
        public int Y { get; } = -142479;
        public int Z { get; } = -1336;
    }
}
