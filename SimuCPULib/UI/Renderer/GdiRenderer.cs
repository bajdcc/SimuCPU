﻿using System.Drawing;
using SimuCPULib.UI.Drawing;

namespace SimuCPULib.UI.Renderer
{
	public class GdiRenderer<T, U> : GraphicsRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected virtual void CreateGdiObject(Graphics graphics)
		{

		}

		protected virtual void DestroyGdiObject(Graphics graphics)
		{

		}

		protected override void _Start()
		{
			_Create();
		}

		protected override void _Stop()
		{
			_Destroy();
		}

		protected void _Destroy()
		{
			DestroyGdiObject(_graphics);
		}

		protected void _Create()
		{
			CreateGdiObject(_graphics);
		}

		protected override void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics)
		{
			_Destroy();
			_Create();
		}
	}
}
