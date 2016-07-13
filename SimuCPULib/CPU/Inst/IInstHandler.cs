using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Core;

namespace SimuCPULib.CPU.Inst
{
    public interface IInstHandler
    {
        string Prefex { get; }

        int OpSize { get; }

        bool Handle(string[] op, Machine machine, CPUContext ctx);
    }
}
