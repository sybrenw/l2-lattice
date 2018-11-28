using Lattice.L2Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Lattice.L2Common.Model
{
    public class Character : L2Object
    {
        // Base account
        public int AccountId { get; set; }
        // Character index
        public int CharacterIndex { get; set; }
        // Character name
        public string Name { get; set; }
        
        // Appearance
        public byte Face { get; set; }
        public byte HairColor { get; set; }
        public byte HairStyle { get; set; }
        public Sex Sex { get; set; }
        public Race Race { get; set; }      
        public int VisibleClassId { get; set; }

        // Active (sub)class
        public int ActiveClassId { get; set; }

        // Level etc
        public byte Level { get; set; }

    }
}
