﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
