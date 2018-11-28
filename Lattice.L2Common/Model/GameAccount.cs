using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lattice.L2Common.Model
{
    public class GameAccount
    {
        /* From loginserver */
        [Key]
        public int AccountId { get; set; }
        public int AccessLevel { get; set; }

        /* GameServer specific */
        public int PinCode { get; set; }
        [NotMapped]
        public List<Character> Characters { get; set; }
    }
}
