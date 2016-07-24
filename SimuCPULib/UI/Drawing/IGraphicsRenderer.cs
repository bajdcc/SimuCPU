using System.Drawing;

namespace SimuCPULib.UI.Drawing
{
	public interface IGraphicsRenderer
	{
		IGraphicsRendererFactory GetFactory();
		void Start(IGraphicsElement element);
		void Stop();
		void SetGraphics(Graphics graphics);
		void Render(Rectangle bound);
		void OnElementStateChanged(int state, object value);
	}
}
