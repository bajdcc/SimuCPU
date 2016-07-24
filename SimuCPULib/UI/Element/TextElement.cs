using System.Drawing;
using System.Drawing.Text;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Global;

namespace SimuCPULib.UI.Element
{
	/// <summary>
	/// 文字图元
	/// </summary>
	public class TextElement : GraphicsElement<TextElement>
	{
		public TextElement()
		{
			this[GraphicsDefines.Text_Color] = Color.Black;
			this[GraphicsDefines.Text_Text] = string.Empty;
			this[GraphicsDefines.Text_Family] = CreateFontFamily();
			this[GraphicsDefines.Text_Size] = 16.0f;
			this[GraphicsDefines.Text_Format] = CreateStringFormat();
		}

		private static FontFamily CreateFontFamily()
		{
			var fontFamily = new FontFamily(GenericFontFamilies.Serif);
			return fontFamily;
		}

		private static StringFormat CreateStringFormat()
		{
			var stringFormat = new StringFormat(StringFormat.GenericDefault);
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			return stringFormat;
		}

		public StringAlignment AlignmentH
		{
			set
			{
				var val = (StringFormat)this[GraphicsDefines.Text_Format];
				val.Alignment = value;
				this[GraphicsDefines.Text_Format] = val;
			}
		}

		public StringAlignment AlignmentV
		{
			set
			{
				var val = (StringFormat)this[GraphicsDefines.Text_Format];
				val.LineAlignment = value;
				this[GraphicsDefines.Text_Format] = val;
			}
		}
	}
}
