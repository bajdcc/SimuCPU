using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;

namespace SimuCPULib.CPU.Inst
{
    class InsHalt : IInstHandler
    {
        public string Prefex => "Halt";

        public int OpSize => 0;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            ctx.Flag.Halt = true;
            return false;
        }
    }
}
