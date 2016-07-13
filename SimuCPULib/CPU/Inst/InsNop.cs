using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;

namespace SimuCPULib.CPU.Inst
{
    class InsNop : IInstHandler
    {
        public string Prefex => "Nop";

        public int OpSize => 0;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            return true;
        }
    }
}
