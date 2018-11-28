using System;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Common.Model.Stats
{
    public class CharStats
    {
        public int HP { get; set; }
        public int MP { get; set; }
        public int CP { get; set; }

        public int[] BStats { get; set; }
        public float[] Stats { get; set; }
        public int[] Elements { get; set; }
    }

    public enum BaseStats
    {
        STR,
        DEX,
        CON,
        INT,
        WIT,
        MEN,
        CHA,
        LUC
    }

    public enum Stats
    {
        // Visible stats
        HP,
        MP,
        CP,

        // Physical
        PAtk,
        PAtkSpeed,
        PDef,
        PEvasion,
        PAccuracy,
        PCritRate,
        PCritDmg,
        PSkillDmg,
        pSkillCritRate,
        pSkillCritDmg,


        // Magical
        MAtk,
        MAtkSpeed,
        MDef,
        MEvasion,
        MAccuracy,
        MCritRate,
        MSkillDmg,
        MSkillCritRate,
        MSkillCritDmg,


    }

    public enum Elements
    {
        Fire,
        Water,
        Wind,
        Earth,
        Holy,
        Dark
    }
}
