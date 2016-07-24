using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Interrupt;

namespace SimuCPULib.CPU.Inst
{
    class InsInt : IInstHandler
    {
        public string Prefex => "Int";

        public int OpSize => 1;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            return IntFactory.HandleInt(machine, ctx, op[0]);
        }
    }
}
