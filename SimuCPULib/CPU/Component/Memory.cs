using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimuCPULib.CPU.Exception;

namespace SimuCPULib.CPU.Component
{
    public class Memory
    {
        private byte[] _memory;

        public Memory(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size.ToString());
            }
            _memory = new byte[size];
        }

        private void ValidAddress(int address, int length)
        {
            if (address < 0 || ((address + length - 1) > _memory.Length))
                throw new CPURuntimeException(CPURuntimeErr.InvalidAddress, address.ToString());
        }

        public uint GetUInt32(int address)
        {
            ValidAddress(address, 4);
            return BitConverter.ToUInt32(_memory, address);
        }

        public int GetInt32(int address)
        {
            ValidAddress(address, 4);
            return BitConverter.ToInt32(_memory, address);
        }

        public ushort GetUInt16(int address)
        {
            ValidAddress(address, 2);
            return BitConverter.ToUInt16(_memory, address);
        }

        public short GetInt16(int address)
        {
            ValidAddress(address, 2);
            return BitConverter.ToInt16(_memory, address);
        }

        public byte GetUInt8(int address)
        {
            ValidAddress(address, 1);
            return _memory[address];
        }

        public char GetInt8(int address)
        {
            ValidAddress(address, 1);
            return BitConverter.ToChar(_memory, address);
        }
    }
}
