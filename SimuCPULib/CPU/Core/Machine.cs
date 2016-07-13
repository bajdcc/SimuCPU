using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimuCPULib.CPU.Component;
using SimuCPULib.CPU.Exception;
using SimuCPULib.CPU.Inst;
using SimuCPULib.GPU.Core;

namespace SimuCPULib.CPU.Core
{
    public class Machine
    {
        public Memory Memory { set; get; } = new Memory(CPUDefines.MEMORY_SIZE);

        public IGPUDisplay Display { set; get; }

        public CPUContext Compile(string inst)
        {
            // TODO: Virtual File System
            if (string.IsNullOrEmpty(inst))
            {
                throw new ArgumentException(nameof(inst));
            }
            var lines = Regex.Split(inst, @"(?:\r|\n|\r\n)+").ToList();
            var labels = lines.Zip(Enumerable.Range(0, lines.Count), (a, b) => new
                {
                    L = a, Index = b
                })
                .Where(a => a.L.StartsWith(CPUDefines.LABEL_PREFIX))
                .Select(a => new { Name = a.L.Substring(1).ToLower(), Index = a.Index})
                .ToDictionary(a => a.Name, a => a.Index);
            return new CPUContext
            {
                Code =
                {
                    Codes = lines,
                    Labels = labels
                }
            };
        }

        public void Step(CPUContext ctx)
        {
            if (ctx.Flag.Halt)
                return;
            int ip = Convert.ToInt32((ctx.Reg.cs.Bit32 << 4) + ctx.Reg.eip.Bit32);
            if (ip >= ctx.Code.Codes.Count)
                throw new CPURuntimeException(CPURuntimeErr.AccessViolation, ip.ToString());
            var inst = ctx.Code.Codes[ip];
            Trace.WriteLine($"[{ip}] {inst}", this.GetType().Name);
            var advance = true;
            if (!inst.StartsWith(CPUDefines.LABEL_PREFIX))
                advance = HandleInst(ctx, inst);
            if (advance && ip < ctx.Code.Codes.Count)
                ctx.Reg.eip.Bit32++;
        }

        static Regex reInst = new Regex(@"(\w+)(.*)", RegexOptions.Compiled);

        private bool HandleInst(CPUContext ctx, string inst)
        {
            var m = reInst.Match(inst);
            if (m.Success)
            {
                var i = m.Groups[1].Value;
                var op = m.Groups[2].Value.Trim();
                return InstFactory.HandleInst(this, ctx, i, op);
            }
            else
            {
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, inst.ToString());
            }
        }
    }
}
