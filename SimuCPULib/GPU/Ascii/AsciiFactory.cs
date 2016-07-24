using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using SimuCPULib.GPU.Core;

namespace SimuCPULib.GPU.Ascii
{
    public static class AsciiFactory
    {
        private static readonly Dictionary<char, byte[]> MapAscii = new Dictionary<char, byte[]>();
        private static readonly byte[] Default;

        static AsciiFactory()
        {
            Default = CreateFontBuilder().FontBytes;
            (XmlHelper.LoadFromResource(typeof(AsciiObject)) as AsciiObject)?.AddAscii();
        }

        public static byte[] GetBytesOfAscii(char ch)
        {
            return MapAscii.ContainsKey(ch) ? MapAscii[ch] : Default;
        }

        public static FontBuilder CreateFontBuilder()
        {
            return new FontBuilder(GPUDefines.FontSize);
        }

        public static void RegisterAsciiFont(char ch, byte[] bytes)
        {
            MapAscii.Add(ch, bytes);
        }
    }
}
