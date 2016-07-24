using System.Drawing;
using SimuCPULib.UI.Element;
using SimuCPULib.UI.Global;

namespace SimuCPULib.UI.Renderer
{
	public class BackgroundElementRenderer : SolidBrushRenderer<BackgroundElementRenderer, BackgroundElement>
	{
		protected override void _Render(Rectangle bound)
		{
			var shape = (ShapeType)_element[GraphicsDefines.Background_Shape];
			var brush = this[GraphicsDefines.SolidBrush_Handle] as Brush;
			switch (shape)
			{
				case ShapeType.Rectangle:
					_graphics.FillRectangle(brush, bound);
					break;
				case ShapeType.Ellipse:
					_graphics.FillEllipse(brush, bound);
					break;
				default:
					break;
			}
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Background_Color:
				case GraphicsDefines.Background_Shape:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}
	}
}
