using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Parser;

namespace SimuCPULib.CPU.Inst
{
    class InsJump : IInstHandler
    {
        public string Prefex => "Jmp";

        public int OpSize => 1;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            ctx.Reg.eip.Bit32 = OpParser.Parse(machine, ctx, op[0].ToLower());
            return false;
        }
    }
}
