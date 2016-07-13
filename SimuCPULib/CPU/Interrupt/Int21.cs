using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.Common.Simulator;
using SimuCPULib.CPU.Core;
using SimuCPULib.GPU.Core;

namespace SimuCPULib.CPU.Interrupt
{
    class Int21 : IIntHandler
    {
        public uint Code => 0x21;

        public bool Handle(Machine machine, CPUContext ctx)
        {
            if (ctx.Reg.eax.Bit8H == 2)
            {
                var ch = Convert.ToChar(ctx.Reg.edx.Bit8L);
                DisplayHelper.ShowChar(machine.Display, ch);
            }
            return true;
        }
    }
}
