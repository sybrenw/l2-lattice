using Lattice.L2Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Common.Model
{
    public class CharacterClass
    {
        public int ClassId { get; set; }

        public Race Race { get; set; }

        public int STR { get; set; }
        public int DEX { get; set; }
        public int CON { get; set; }
        public int INT { get; set; }
        public int WIT { get; set; }
        public int MEN { get; set; }
    }
}
