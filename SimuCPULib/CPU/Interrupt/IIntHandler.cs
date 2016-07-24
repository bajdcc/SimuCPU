using SimuCPULib.CPU.Core;

namespace SimuCPULib.CPU.Interrupt
{
    public interface IIntHandler
    {
        uint Code { get; }

        bool Handle(Machine machine, CPUContext ctx);
    }
}
