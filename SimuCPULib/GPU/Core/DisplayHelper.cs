using System.Diagnostics;
using System.Drawing;
using SimuCPULib.GPU.Ascii;

namespace SimuCPULib.GPU.Core
{
    public static class DisplayHelper
    {
        public static void ShowChar(IGPUDisplay display, char ch)
        {
            Trace.WriteLine($"[GPU] Char={ch}", nameof(DisplayHelper));
            var bytes = AsciiFactory.GetBytesOfAscii(ch);
            var size = display.GetFontSize();
            var fg = display.GetFgColor().ToArgb();
            var bk = display.GetBkColor().ToArgb();
            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    display.SetPixel(new Point(i, j), bytes[i*size.Width+j] != 0 ? fg : bk);
                }
            }
        }
    }
}
