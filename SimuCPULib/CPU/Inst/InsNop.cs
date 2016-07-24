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
