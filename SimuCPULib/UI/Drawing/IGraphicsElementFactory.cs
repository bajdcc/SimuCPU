﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCPULib.UI.Drawing
{
	public interface IGraphicsElementFactory
	{
		IGraphicsElement Create();
	}
}