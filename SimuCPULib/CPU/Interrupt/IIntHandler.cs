using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;

namespace SimuCPULib.CPU.Interrupt
{
    public interface IIntHandler
    {
        uint Code { get; }

        bool Handle(Machine machine, CPUContext ctx);
    }
}
