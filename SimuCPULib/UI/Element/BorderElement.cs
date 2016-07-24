using System.Drawing.Drawing2D;
using SimuCPULib.Common.Simulator;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Global;

namespace SimuCPULib.UI.Element
{
	/// <summary>
	/// 边框图元
	/// </summary>
	public class BorderElement: GraphicsElement<BorderElement>
	{
		public BorderElement()
		{
			this[GraphicsDefines.Border_Color] = Constants.NormalBorder;
			this[GraphicsDefines.Border_Width] = 1.6f;
			this[GraphicsDefines.Border_Style] = DashStyle.Solid;
			this[GraphicsDefines.Border_Join] = LineJoin.Round;
			this[GraphicsDefines.Border_Shape] = ShapeType.Rectangle;
			this[GraphicsDefines.Border_Focus] = false;
			this[GraphicsDefines.Border_Hover] = false;
		}
	}
}
