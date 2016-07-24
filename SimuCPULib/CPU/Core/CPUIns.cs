namespace SimuCPULib.CPU.Core
{
    public enum CPUIns
    {
        Nop,
        Halt,
        Debug,
        Int,

        Jump,
        JumpZero,
        JumpNonZero,
        JumpAbove,
        JumpAboveEqual,

        Push,
        Pop,

        Loop,

        Inc,
        Dec,

        Move,

        ShiftLeft,
        ShiftRight,

        Compare
    }
}
