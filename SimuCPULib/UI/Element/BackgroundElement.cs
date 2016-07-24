using System.Drawing;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Global;

namespace SimuCPULib.UI.Element
{
	/// <summary>
	/// 背景颜色图元
	/// </summary>
	public class BackgroundElement: GraphicsElement<BackgroundElement>
	{
		public BackgroundElement()
		{
			this[GraphicsDefines.Background_Color] = Color.Black;
			this[GraphicsDefines.Background_Shape] = ShapeType.Rectangle;
		}
	}
}
