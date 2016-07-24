using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Element;
using SimuCPULib.UI.Renderer;

namespace SimuCPULib.UI.Global
{
	public static class Storage
	{
	    public static Dictionary<string, IGraphicsRendererFactory> RendererFactory { get; set; } = new Dictionary<string, IGraphicsRendererFactory>();

	    private static Dictionary<string, IGraphicsElementFactory> _elementFactory = new Dictionary<string, IGraphicsElementFactory>();
		public static Dictionary<string, IGraphicsElementFactory> ElementFactory
		{
			get { return _elementFactory; }
			set { _elementFactory = value; }
		}

	    public static string CPUInst { set; get; } = string.Empty;

		private static Graphics _graphics;

		/// <summary>
		/// 绘制API
		/// </summary>
		public static Graphics Graphics => _graphics;

	    private static Bitmap _bitmap;

		/// <summary>
		/// 缓冲位图
		/// </summary>
		public static Bitmap Bitmap => _bitmap;

	    private static Size _size;

		public static Size Size
		{
			get { return _size; }
			set { _size = value; }
		}

		private static Control _ctrl;

		public static Control Ctrl
		{
			get { return _ctrl; }
			set { _ctrl = value; }
		}

		private static ToolTip _tip;

		public static ToolTip Tip
		{
			get { return _tip; }
			set { _tip = value; }
		}

		private static Timer _delay;

		public static Timer Delay => _delay;

	    public static Point MousePosition => _ctrl.PointToClient(Control.MousePosition);

	    static Storage()
		{
			RegisterRenderer();			
		}

		private static void RegisterRenderer()
		{
			BorderElementRenderer.Register();
			BackgroundElementRenderer.Register();
			GradientBackgroundElementRenderer.Register();
			TextElementRenderer.Register();
			LineElementRenderer.Register();
			PixelElementRenderer.Register();

			BorderElement.Register();
			BackgroundElement.Register();
			GradientBackgroundElement.Register();
			TextElement.Register();
			LineElement.Register();
			PixelElement.Register();
		}

		public static void InitializeGui(Size size)
		{
			if (_graphics != null)
			{
				_graphics.Dispose();
				_graphics = null;
				_bitmap.Dispose();
				_bitmap = null;
			}
			_size = size;
			_bitmap = new Bitmap(size.Width, size.Height);
			_graphics = Graphics.FromImage(_bitmap);
			_graphics.SmoothingMode = SmoothingMode.AntiAlias;
			_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			_graphics.CompositingQuality = CompositingQuality.HighQuality;
			_delay = new Timer();
		}
	}
}
