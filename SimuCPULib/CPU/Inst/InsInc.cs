using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Parser;

namespace SimuCPULib.CPU.Inst
{
    class InsInc : IInstHandler
    {
        public string Prefex => "Inc";

        public int OpSize => 1;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            var code = OpParser.Parse(machine, ctx, op[0]);
            code++;
            OpParser.Assign(machine, ctx, op[0], code);
            return true;
        }
    }
}
