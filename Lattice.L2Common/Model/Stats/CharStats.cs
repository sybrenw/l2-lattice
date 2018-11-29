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

        public double RunSpeedMultiplier { get; set; } = 1.0;
        public double AttackSpeedMultiplier { get; set; } = 1.0;

        public short[] BaseStats { get; set; } = new short[8];
        public float[] Stats { get; set; } = new float[(int)StatType.Length];
        public short[] DefElements { get; set; } = new short[6];
        public short[] AtkElements { get; set; } = new short[6];

        public short this[BaseStatType type]
        {
            get
            {
                return BaseStats[(int)type];
            }
        }

        public int this[StatType type]
        {
            get
            {
                return (int)Stats[(int)type];
            }
        }

        public static CharStats CreateCharStats()
        {
            return new CharStats()
            {
                HP = 10000,
                MP = 10000,
                CP = 10000,
                BaseStats = new short[] { 80, 80, 80, 80, 80, 80, 80, 80 },
                DefElements = new short[] { 300, 300, 300, 300, 300, 300 },
                AtkElements = new short[] { 300, 300, 300, 300, 300, 300 },
                Stats = CreateStats(200, 1000)
            };
        }

        private static float[] CreateStats(int fac1, int fac2)
        {
            float[] values = new float[(int)StatType.Length];

            for (int i = 0; i < values.Length; i++)
                values[i] = fac1;

            return values;
        }
    }

    public enum BaseStatType
    {
        STR,
        DEX,
        CON,
        INT,
        WIT,
        MEN,
        CHA,
        LUC,
    }

    public enum StatType
    {
        // Visible stats
        MaxHP,
        MaxMP,
        MaxCP,

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
        CastingSpeed,

        // Movement
        RunSpeedSlow,
        RunSpeed,
        SwimSpeedSlow,
        SwimSpeed,
        MountSpeedSlow,
        MountSpeed,
        FlySpeedSlow,
        FlySpeed,

        Length,
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
