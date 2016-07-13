using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.GPU.Core
{
    public static class DisplayHelper
    {
        private static Font _font = new Font(new FontFamily("微软雅黑"), 10);

        public static void ShowChar(IGPUDisplay display, char ch)
        {
            Trace.WriteLine($"[GPU] Char={ch}", nameof(DisplayHelper));
            var size = display.GetSize();
            var bmp = new Bitmap(size.Width, size.Height);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.DrawString(ch.ToString(), _font, Brushes.Black, new PointF(0, 0));
            foreach (var pt in Enumerable.Range(0, size.Width * size.Height)
                .Select(a => new Point(a % size.Width, a / size.Height)))
            {
                display.SetPixel(pt, bmp.GetPixel(pt.X, pt.Y).GetBrightness() > 0.5f ? Color.Black.ToArgb() : Color.White.ToArgb());
            }
        }
    }
}
