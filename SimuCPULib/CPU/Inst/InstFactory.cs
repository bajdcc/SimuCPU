using System.Collections.Generic;
using System.Linq;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Exception;

namespace SimuCPULib.CPU.Inst
{
    public static class InstFactory
    {
        private static Dictionary<CPUIns, IInstHandler> _handlers = new Dictionary<CPUIns, IInstHandler>();
        private static Dictionary<string, CPUIns> _filters = new Dictionary<string, CPUIns>();

        static InstFactory()
        {
            RegisterHandlers();
            RegisterFilters();
        }

        private static void RegisterHandlers()
        {
            _handlers.Add(CPUIns.Nop, new InsNop());
            _handlers.Add(CPUIns.Halt, new InsHalt());
            _handlers.Add(CPUIns.Move, new InsMove());
            _handlers.Add(CPUIns.Int, new InsInt());
            _handlers.Add(CPUIns.Jump, new InsJump());
            _handlers.Add(CPUIns.Inc, new InsInc());
        }

        private static void RegisterFilters()
        {
            foreach (var handler in _handlers)
            {
                _filters.Add(handler.Value.Prefex.ToLower(), handler.Key);
            }
        }

        public static bool HandleInst(Machine machine, CPUContext ctx, string inst, string op)
        {
            if (!_filters.ContainsKey(inst))
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, string.Concat(inst, " ", op));
            var type = _filters[inst.ToLower()];
            var handler = _handlers[type];
            var operand = string.IsNullOrEmpty(op) ? new string[0] : op.Split(',').Select(a => a.Replace(" ", "")).ToArray();
            if (operand.Length != handler.OpSize)
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, string.Concat(inst, " ", op));
            return handler.Handle(operand, machine, ctx);
        }
    }
}
