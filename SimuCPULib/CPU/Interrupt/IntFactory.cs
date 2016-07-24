using System.Collections.Generic;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Exception;
using SimuCPULib.CPU.Parser;

namespace SimuCPULib.CPU.Interrupt
{
    public class IntFactory
    {
        private static Dictionary<uint, IIntHandler> _handlers = new Dictionary<uint, IIntHandler>();

        static IntFactory()
        {
            RegisterHandlers();
        }

        private static void RegisterHandlers()
        {
            var list = new IIntHandler[]
            {
                new Int21() 
            };
            foreach (var i in list)
            {
                _handlers.Add(i.Code, i);
            }
        }

        public static bool HandleInt(Machine machine, CPUContext ctx, string code)
        {
            var i = OpParser.Parse(machine, ctx, code);
            if (!_handlers.ContainsKey(i))
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, code);
            var handler = _handlers[i];
            return handler.Handle(machine, ctx);
        }
    }
}
