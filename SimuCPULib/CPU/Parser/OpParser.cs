using System;
using System.Text.RegularExpressions;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Exception;

namespace SimuCPULib.CPU.Parser
{
    public enum OpType
    {
        Register,
        Memory
    }

    public static class OpParser
    {
        private static Regex reIntNumber = new Regex(@"^(\d+)(h?)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex reIntChar = new Regex(@"^'(\w)'?$", RegexOptions.Compiled);
        private static Regex reIntReg = new Regex(@"^([a-zA-Z]{2,3})$", RegexOptions.Compiled);

        public static uint Parse(Machine machine, CPUContext ctx, string op)
        {
            Match m = reIntNumber.Match(op);
            if (m.Success)
            {
                return Convert.ToUInt32(m.Groups[1].Value, m.Groups[2].Success ? 16 : 10);
            }
            m = reIntReg.Match(op);
            if (m.Success)
            {
                return ctx.Reg.Get(m.Groups[1].Value);
            }
            m = reIntChar.Match(op);
            if (m.Success)
            {
                return m.Groups[1].Value.ToCharArray()[0];
            }
            if (ctx.Code.Labels.ContainsKey(op))
            {
                return Convert.ToUInt32(ctx.Code.Labels[op]);
            }
            throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, op);
        }

        public static void Assign(Machine machine, CPUContext ctx, string dst, string src)
        {
            var s = Parse(machine, ctx, src);
            var reg = reIntReg.Match(dst);
            if (reg.Success)
            {
                ctx.Reg.Set(dst, s);
            }
        }

        public static void Assign(Machine machine, CPUContext ctx, string dst, uint src)
        {
            var reg = reIntReg.Match(dst);
            if (reg.Success)
            {
                ctx.Reg.Set(dst, src);
            }
        }
    }
}
