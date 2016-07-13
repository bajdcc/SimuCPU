using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.CPU.Core
{
    public class CPUContext
    {
        public CPUReg Reg { set; get; } = new CPUReg();

        public CPUFlag Flag { set; get; } = new CPUFlag();

        public CPUCode Code { set; get; } = new CPUCode();
    }
}
