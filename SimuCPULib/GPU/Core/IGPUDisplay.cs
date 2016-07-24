using System.Drawing;

namespace SimuCPULib.GPU.Core
{
    public interface IGPUDisplay
    {
        Size GetFontSize();

        Size GetScreenSize();

        void SetPixel(Point point, int color);

        Color GetFgColor();

        Color GetBkColor();
    }
}
