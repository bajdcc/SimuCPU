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
