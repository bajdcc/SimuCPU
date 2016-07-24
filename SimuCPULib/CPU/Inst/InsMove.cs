using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Parser;

namespace SimuCPULib.CPU.Inst
{
    class InsMove : IInstHandler
    {
        public string Prefex => "Mov";

        public int OpSize => 2;

        public bool Handle(string[] op, Machine machine, CPUContext ctx)
        {
            OpParser.Assign(machine, ctx, op[0], op[1]);
            return true;
        }
    }
}
