using System.Drawing;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Global;

namespace SimuCPULib.UI.Element
{
	/// <summary>
	/// 渐变颜色图元
	/// </summary>
	public class GradientBackgroundElement : GraphicsElement<GradientBackgroundElement>
	{
		public GradientBackgroundElement()
		{
			this[GraphicsDefines.GradientBackground_ColorBegin] = Color.Black;
			this[GraphicsDefines.GradientBackground_ColorEnd] = Color.White;
			this[GraphicsDefines.GradientBackground_PointBegin] = Point.Empty;
			this[GraphicsDefines.GradientBackground_PointEnd] = Point.Empty;
			this[GraphicsDefines.GradientBackground_Direction] = GradientType.Horizontal;
			this[GraphicsDefines.GradientBackground_Shape] = ShapeType.Rectangle;
		}
	}
}
