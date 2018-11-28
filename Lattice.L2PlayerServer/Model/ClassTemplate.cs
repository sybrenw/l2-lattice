using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2PlayerServer.Model
{
    public class ClassTemplate
    {
        public int Race { get; set; }
        public int Class { get; set; }
        public Stats Stats { get; set; }
    }

    public class Stats
    {
        public static readonly Stats Default = new Stats()
        {
            STR = 88,
            DEX = 55,
            CON = 82,
            INT = 39,
            WIT = 39,
            MEN = 38
        };

        public int STR { get; set; }
        public int DEX { get; set; }
        public int CON { get; set; }
        public int INT { get; set; }
        public int WIT { get; set; }
        public int MEN { get; set; }
    }
}
