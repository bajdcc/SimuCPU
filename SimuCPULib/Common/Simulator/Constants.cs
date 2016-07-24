using System.Drawing;

namespace SimuCPULib.Common.Simulator
{
	public static class Constants
	{
		public static readonly Color WindowBackground = Color.WhiteSmoke;

        public static readonly Color FocusBorder = Color.Gray;
        public static readonly Color HoverBorder = Color.DarkGray;
        public static readonly Color NormalBorder = Color.LightGray;

        public static int Initialize()
		{
		    return WindowBackground.ToArgb();
		}
	}

    public enum Direction
    {
        Left,
        Up,
        Right,
        Bottom
    }

    public enum HandleType
	{
		Test,
		LeftDown,
		MidDown,
		RightDown,
		LeftUp,
		MidUp,
		RightUp,
		Enter,
		Hover,
		Leave,
		Move,
		Focus,
		LostFocus,
		Drag
	}
}
