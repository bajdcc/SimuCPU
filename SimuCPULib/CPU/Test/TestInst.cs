using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.CPU.Test
{
    public static class TestInst
    {
        public static string Inst => "nop\n" +
                                     "mov ah,02h\n" +
                                     "mov dl,'0'\n" +
                                     ".LOOP\n" +
                                     "int 21h\n" +
                                     "inc dl\n" +
                                     "jmp loop\n" +
                                     "halt\n";
    }
}
