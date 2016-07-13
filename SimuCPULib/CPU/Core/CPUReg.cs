using System;
using System.Collections.Generic;
using SimuCPULib.CPU.Exception;

namespace SimuCPULib.CPU.Core
{
    public class CPUReg
    {
        private Dictionary<string, Action<uint>> _mapRegSet = new Dictionary<string, Action<uint>>();
        private Dictionary<string, Func<uint>> _mapRegGet = new Dictionary<string, Func<uint>>();

        public CPUReg()
        {
            _mapRegSet.Add("eax", a => eax.Bit32 = a);
            _mapRegSet.Add("ax", a => eax.Bit16 = Convert.ToUInt16(a & 0xFFFF));
            _mapRegSet.Add("al", a => eax.Bit8L = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("ah", a => eax.Bit8H = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("ebx", a => ebx.Bit32 = a);
            _mapRegSet.Add("bx", a => ebx.Bit16 = Convert.ToUInt16(a & 0xFFFF));
            _mapRegSet.Add("bl", a => ebx.Bit8L = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("bh", a => ebx.Bit8H = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("ecx", a => ecx.Bit32 = a);
            _mapRegSet.Add("cx", a => ecx.Bit16 = Convert.ToUInt16(a & 0xFFFF));
            _mapRegSet.Add("cl", a => ecx.Bit8L = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("ch", a => ecx.Bit8H = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("edx", a => edx.Bit32 = a);
            _mapRegSet.Add("dx", a => edx.Bit16 = Convert.ToUInt16(a & 0xFFFF));
            _mapRegSet.Add("dl", a => edx.Bit8L = Convert.ToByte(a & 0xFF));
            _mapRegSet.Add("dh", a => edx.Bit8H = Convert.ToByte(a & 0xFF));

            _mapRegGet.Add("eax", () => eax.Bit32);
            _mapRegGet.Add("ax", () => eax.Bit16);
            _mapRegGet.Add("al", () => eax.Bit8L);
            _mapRegGet.Add("ah", () => eax.Bit8H);
            _mapRegGet.Add("ebx",() => ebx.Bit32);
            _mapRegGet.Add("bx", () => ebx.Bit16);
            _mapRegGet.Add("bl", () => ebx.Bit8L);
            _mapRegGet.Add("bh", () => ebx.Bit8H);
            _mapRegGet.Add("ecx",() => ecx.Bit32);
            _mapRegGet.Add("cx", () => ecx.Bit16);
            _mapRegGet.Add("cl", () => ecx.Bit8L);
            _mapRegGet.Add("ch", () => ecx.Bit8H);
            _mapRegGet.Add("edx",() => edx.Bit32);
            _mapRegGet.Add("dx", () => edx.Bit16);
            _mapRegGet.Add("dl", () => edx.Bit8L);
            _mapRegGet.Add("dh", () => edx.Bit8H);
        }

        public void Set(string name, uint val)
        {
            if (!_mapRegSet.ContainsKey(name))
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, name);
            _mapRegSet[name](val);
        }

        public uint Get(string name)
        {
            if (!_mapRegGet.ContainsKey(name))
                throw new CPURuntimeException(CPURuntimeErr.InvalidInstruction, name);
            return _mapRegGet[name]();
        }

        public CPURegister eax { set; get; } = new CPURegister();

        public CPURegister ebx { set; get; } = new CPURegister();

        public CPURegister ecx { set; get; } = new CPURegister();

        public CPURegister edx { set; get; } = new CPURegister();

        public CPURegister esp { set; get; } = new CPURegister();

        public CPURegister ebp { set; get; } = new CPURegister();

        public CPURegister edi { set; get; } = new CPURegister();

        public CPURegister esi { set; get; } = new CPURegister();

        public CPURegister eip { set; get; } = new CPURegister();

        public CPURegister cs { set; get; } = new CPURegister();

        public CPURegister es { set; get; } = new CPURegister();

        public CPURegister ds { set; get; } = new CPURegister();

        public CPURegister ss { set; get; } = new CPURegister();

        public CPURegister fs { set; get; } = new CPURegister();

        public CPURegister gs { set; get; } = new CPURegister();
    }

    public class CPURegister
    {
        private uint Bytes { set; get; } = 0;

        public uint Bit32 {
            set { Bytes = value; }
            get { return Bytes; }
        }

        public ushort Bit16
        {
            set { Bytes = ((Bytes & 0xFFFF0000) | value); }
            get { return Convert.ToUInt16(Bytes & 0xFFFF); }
        }

        public byte Bit8L
        {
            set { Bytes = ((Bytes & 0xFFFFFF00) | value); }
            get { return Convert.ToByte(Bytes & 0xFF); }
        }

        public byte Bit8H
        {
            set { Bytes = ((Bytes & 0xFFFF00FF) | ((uint)value) << 8); }
            get { return Convert.ToByte((Bytes & 0xFF00) >> 8); }
        }
    }
}