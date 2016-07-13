using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.CPU.Exception
{
    public enum CPURuntimeErr
    {
        AccessViolation,
        InvalidAddress,
        InvalidLabel,
        InvalidInstruction,
    }

    public class CPURuntimeException : System.Exception
    {
        public CPURuntimeErr Error { set; get; }

        public CPURuntimeException(CPURuntimeErr err, string message) : base(message)
        {
            Error = err;
        }

        public override string ToString()
        {
            return string.Concat(ErrToString(Error), ": ",  base.ToString());
        }

        static string ErrToString(CPURuntimeErr err)
        {
            return err.ToString();
        }
    }
}
