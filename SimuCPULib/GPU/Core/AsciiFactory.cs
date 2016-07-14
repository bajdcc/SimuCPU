using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.GPU.Core
{
    public static class AsciiFactory
    {
        private static readonly Dictionary<char, byte[]> MapAscii = new Dictionary<char, byte[]>();
        private static readonly byte[] Default;

        static AsciiFactory()
        {
            Default = CreateFontBuilder().FontBytes;
            RegisterAsciiFont('0', CreateFontBuilder()
                    .BeginLine()
                    .From(new Point(2, 3))
                    .To(new Point(4, 1))
                    .To(new Point(12, 1))
                    .To(new Point(14, 3))
                    .End()
                    .CopyAndFlipH(1, 4, 3)
                    .FontBytes);
            RegisterAsciiFont('1', CreateFontBuilder()
                    .BeginLine()
                    .From(new Point(2, 4))
                    .To(new Point(14, 4))
                    .From(new Point(3, 2))
                    .To(new Point(3, 3))
                    .End()
                    .FontBytes);
        }

        public static byte[] GetBytesOfAscii(char ch)
        {
            return MapAscii.ContainsKey(ch) ? MapAscii[ch] : Default;
        }

        private static FontBuilder CreateFontBuilder()
        {
            return new FontBuilder(GPUDefines.FontSize);
        }

        private static void RegisterAsciiFont(char ch, byte[] bytes)
        {
            MapAscii.Add(ch, bytes);
        }
    }
}
