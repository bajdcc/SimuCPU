using System;

namespace SimuCPULib.Common.Simulator
{
    public static class Defines
    {
        public const int NODE_SMALL = 0;
        public const int NODE_OFFSET_X = 10;
        public const int NODE_OFFSET_Y = 10;
        public const int NODE_WIDTH = 32;
        public const int NODE_HEIGHT = 32;
        public const int WIDTH_COUNT = 16;
        public const int HEIGHT_COUNT = 16;

        public static double Clamp(double t, double min, double max)
        {
            if (t > max)
            {
                return max;
            }
            if (t < min)
            {
                return min;
            }
            return t;
        }

        public static double Clamp(double t, double area)
        {
            return Math.Abs(t) < area ? t : Math.Sign(t)*area;
        }
    }
}