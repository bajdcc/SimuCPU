using System;
using System.Diagnostics;

namespace SimuCPULib.CPU.Core
{
    public enum CPUFlagType
    {
        CF,
        PF,
        AF,
        ZF,
        SF,
        TF,
        IF,
        DF,
        OF,
    }

    public class CPUFlag
    {
        private int bits = 0;

        public bool Halt { set; get; } = false;

        public void Set(CPUFlagType flag, bool value)
        {
            var b = GetBitByType(flag);
            if (b == -1)
                return;
            if (value)
                bits |= (1 << b);
            else
                bits &= ~(1 << b);
        }

        public bool Get(CPUFlagType flag)
        {
            var b = GetBitByType(flag);
            return ((bits >> b) & 1) == 1;
        }

        static int GetBitByType(CPUFlagType flag)
        {
            switch (flag)
            {
                case CPUFlagType.CF:
                    return 0;
                case CPUFlagType.PF:
                    return 2;
                case CPUFlagType.AF:
                    return 4;
                case CPUFlagType.ZF:
                    return 6;
                case CPUFlagType.SF:
                    return 7;
                case CPUFlagType.TF:
                    return 8;
                case CPUFlagType.IF:
                    return 9;
                case CPUFlagType.DF:
                    return 10;
                case CPUFlagType.OF:
                    return 11;
                default:
                    return -1;
            }
        }
    }
}