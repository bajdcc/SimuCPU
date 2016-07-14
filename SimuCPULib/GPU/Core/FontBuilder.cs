using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SimuCPULib.GPU.Core
{
    public class FontBuilder
    {
        private Size FontSize { get; }

        public byte[] FontBytes { get; }

        public FontBuilder(Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException(size.ToString(), nameof(size));
            FontSize = size;
            FontBytes = new byte[size.Width*size.Height];
        }

        public void ValidPoint(Point pt)
        {
            if (pt.X < 0 || pt.Y < 0 || pt.X >= FontSize.Height || pt.Y >= FontSize.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(pt), pt.ToString());
            }
        }

        public void SetPoint(Point pt)
        {
            var index = pt.X*FontSize.Width + pt.Y;
            FontBytes[index] = 1;
        }

        public void SetPoint(Point pt, bool show)
        {
            var index = pt.X*FontSize.Width + pt.Y;
            FontBytes[index] = Convert.ToByte(show);
        }

        public bool GetPoint(Point pt)
        {
            var index = pt.X*FontSize.Width + pt.Y;
            return FontBytes[index] != 0;
        }

        public void CopyPoint(Point src, Point dst)
        {
            SetPoint(dst, GetPoint(src));
        }

        public void SwapPoint(Point pt1, Point pt2)
        {
            var t = GetPoint(pt1);
            SetPoint(pt1, GetPoint(pt2));
            SetPoint(pt2, t);
        }

        public FontBuilderLine BeginLine()
        {
            return new FontBuilderLine(this);
        }

        public FontBuilder CopyV(int src, int dst, int count)
        {
            if (src < 0 || src >= FontSize.Height)
                throw new ArgumentException(src.ToString(), nameof(src));
            if (dst < 0 || dst >= FontSize.Height)
                throw new ArgumentException(dst.ToString(), nameof(dst));
            if (count < 1)
                throw new ArgumentException(count.ToString(), nameof(count));
            if (src == dst) return this;
            if (src < dst)
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    for (var j = 0; j < FontSize.Width; j++)
                    {
                        CopyPoint(new Point(i + src, j), new Point(i + dst, j));
                    }
                }
            }
            else
            {
                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < FontSize.Width; j++)
                    {
                        CopyPoint(new Point(i + src, j), new Point(i + dst, j));
                    }
                }
            }
            return this;
        }

        public FontBuilder CopyH(int src, int dst, int count)
        {
            if (src < 0 || src >= FontSize.Width)
                throw new ArgumentException(src.ToString(), nameof(src));
            if (dst < 0 || dst >= FontSize.Width)
                throw new ArgumentException(dst.ToString(), nameof(dst));
            if (count < 1)
                throw new ArgumentException(count.ToString(), nameof(count));
            if (src == dst) return this;
            if (src < dst)
            {
                for (var i = count - 1; i >= 0; i--)
                {
                    for (var j = 0; j < FontSize.Height; j++)
                    {
                        CopyPoint(new Point(j, i + src), new Point(j, i + dst));
                    }
                }
            }
            else
            {
                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < FontSize.Height; j++)
                    {
                        CopyPoint(new Point(j, i + src), new Point(j, i + dst));
                    }
                }
            }
            return this;
        }

        public FontBuilder FlipV(int src1, int src2)
        {
            if (src1 < 0 || src1 >= FontSize.Height)
                throw new ArgumentException(src1.ToString(), nameof(src1));
            if (src2 < 0 || src2 >= FontSize.Height)
                throw new ArgumentException(src2.ToString(), nameof(src2));
            var min = Math.Min(src1, src2);
            var max = Math.Max(src1, src2);
            var k = max - min;
            if (k == 0)
                return this;
            for (var i = 0; i <= (k - 1)/2; i++)
            {
                for (var j = 0; j < FontSize.Width; j++)
                {
                    SwapPoint(new Point(min + i, j), new Point(max - i, j));
                }
            }
            return this;
        }

        public FontBuilder FlipH(int src1, int src2)
        {
            if (src1 < 0 || src1 >= FontSize.Width)
                throw new ArgumentException(src1.ToString(), nameof(src1));
            if (src2 < 0 || src2 >= FontSize.Width)
                throw new ArgumentException(src2.ToString(), nameof(src2));
            var min = Math.Min(src1, src2);
            var max = Math.Max(src1, src2);
            var k = max - min;
            if (k == 0)
                return this;
            for (var i = 0; i <= (k - 1)/2; i++)
            {
                for (var j = 0; j < FontSize.Height; j++)
                {
                    SwapPoint(new Point(j, min + i), new Point(j, max - i));
                }
            }
            return this;
        }

        public FontBuilder CopyAndFlipV(int src, int dst, int count)
        {
            return CopyV(src, dst, count).FlipV(dst, dst + count - 1);
        }

        public FontBuilder CopyAndFlipH(int src, int dst, int count)
        {
            return CopyH(src, dst, count).FlipH(dst, dst + count - 1);
        }
    }

    public abstract class FontBuilderBase
    {
        protected FontBuilder FontBuilder { get; }

        protected FontBuilderBase(FontBuilder fb)
        {
            FontBuilder = fb;
        }

        public FontBuilder End()
        {
            return FontBuilder;
        }
    }

    public class FontBuilderLine : FontBuilderBase
    {
        private Point _current;

        public FontBuilderLine(FontBuilder fb) : base(fb)
        {
        }

        public FontBuilderLine From(Point pt)
        {
            FontBuilder.ValidPoint(pt);
            FontBuilder.SetPoint(pt);
            _current = pt;
            return this;
        }

        public FontBuilderLine To(Point pt)
        {
            FontBuilder.ValidPoint(pt);
            if (pt == _current)
                return this;
            if (pt.X == _current.X)
            {
                if (pt.Y > _current.Y)
                {
                    for (var i = pt.Y; i > _current.Y; i--)
                    {
                        FontBuilder.SetPoint(new Point(_current.X, i));
                    }
                }
                else
                {
                    for (var i = pt.Y; i < _current.Y; i++)
                    {
                        FontBuilder.SetPoint(new Point(_current.X, i));
                    }
                }
            }
            else if (pt.Y == _current.Y)
            {
                if (pt.X > _current.X)
                {
                    for (var i = pt.X; i > _current.X; i--)
                    {
                        FontBuilder.SetPoint(new Point(i, _current.Y));
                    }
                }
                else
                {
                    for (var i = pt.X; i < _current.X; i++)
                    {
                        FontBuilder.SetPoint(new Point(i, _current.Y));
                    }
                }
            }
            else
            {
                var x = Math.Abs(pt.X - _current.X);
                var y = Math.Abs(pt.Y - _current.Y);
                if (x == 1 || y == 1)
                {
                    FontBuilder.SetPoint(pt);
                }
                else if (x == y)
                {
                    foreach (var i in EnumerableHelper.Range(_current.X, pt.X)
                        .Zip(EnumerableHelper.Range(_current.Y, pt.Y), (a, b) => new Point(a, b)))
                    {
                        FontBuilder.SetPoint(i);
                    }
                }
                else if (x < y && (y%x == 0))
                {
                    foreach (var i in EnumerableHelper.Range(_current.X, pt.X)
                        .Zip(EnumerableHelper.Range(_current.Y, pt.Y).Stretch(y/x), (a, b) => new Point(a, b)))
                    {
                        FontBuilder.SetPoint(i);
                    }
                }
                else if (y < x && (x%y == 0))
                {
                    foreach (var i in EnumerableHelper.Range(_current.X, pt.X)
                        .Zip(EnumerableHelper.Range(_current.Y, pt.Y).Stretch(x/y), (a, b) => new Point(a, b)))
                    {
                        FontBuilder.SetPoint(i);
                    }
                }
                else
                {
                    throw new ArgumentException(pt.ToString(), nameof(pt));
                }
            }
            _current = pt;
            return this;
        }
    }

    public static class EnumerableHelper
    {
        public static IEnumerable<T> Stretch<T>(this IEnumerable<T> source, int count)
        {
            if (count < 1) throw new ArgumentOutOfRangeException(nameof(count), count.ToString());
            foreach (var k in source)
            {
                for (var i = 0; i < count; i++)
                {
                    yield return k;
                }
            }
        }

        public static IEnumerable<int> Range(int from, int to)
        {
            if (from < to)
            {
                for (var i = from; i <= to; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (var i = from; i >= to; i--)
                {
                    yield return i;
                }
            }
        }
    }
}
