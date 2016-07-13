﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCPULib.GPU.Core
{
    public interface IGPUDisplay
    {
        Size GetSize();

        void SetPixel(Point point, int color);
    }
}