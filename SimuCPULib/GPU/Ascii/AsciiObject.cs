using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SimuCPULib.GPU.Core;

namespace SimuCPULib.GPU.Ascii
{
    [XmlRoot("AsciiSettings", Namespace = "http://www.bajdcc.com", IsNullable = false)]
    public class AsciiObject
    {
        [XmlArray("Fonts", IsNullable = false)]
        [XmlArrayItem("Font", Type = typeof(AsciiFont))]
        public AsciiFont[] Fonts { set; get; }

        public void AddAscii()
        {
            foreach (var f in Fonts)
            {
                f.AddAscii();
            }
        }
    }
    
    public class AsciiFont
    {
        [XmlAttribute("Code")]
        public ushort Id { set; get; }

        [XmlArray("Path")]
        [XmlArrayItem("Line", Type = typeof(AsciiBuilderLine))]
        [XmlArrayItem("Copy", Type = typeof(AsciiBuilderCopy))]
        public AsciiBuilderBase[] Path { set; get; }

        public void AddAscii()
        {
            var fb = AsciiFactory.CreateFontBuilder();
            foreach (var p in Path)
            {
                p.AddPath(fb);
            }
            AsciiFactory.RegisterAsciiFont((char)Id, fb.FontBytes);
        }
    }

    public abstract class AsciiBuilderBase
    {
        public abstract void AddPath(FontBuilder fb);
    }

    public class AsciiBuilderLine : AsciiBuilderBase
    {
        [XmlArray("Points", IsNullable = false)]
        [XmlArrayItem("From", Type = typeof(AsciiBuilderLineFrom))]
        [XmlArrayItem("To", Type = typeof(AsciiBuilderLineTo))]
        public AsciiBuilderLineBase[] Points { set; get; }

        public override void AddPath(FontBuilder fb)
        {
            var l = fb.BeginLine();
            foreach (var p in Points)
            {
                p.AddPoint(l);
            }
        }
    }

    public abstract class AsciiBuilderLineBase
    {
        public abstract void AddPoint(FontBuilderLine fontBuilderLine);
    }

    public class AsciiBuilderLineFrom : AsciiBuilderLineBase
    {
        [XmlAttribute("X")]
        public int X { set; get; }

        [XmlAttribute("Y")]
        public int Y { set; get; }

        public override void AddPoint(FontBuilderLine fb)
        {
            fb.From(new Point(X, Y));
        }
    }

    public class AsciiBuilderLineTo : AsciiBuilderLineBase
    {
        [XmlAttribute("X")]
        public int X { set; get; }

        [XmlAttribute("Y")]
        public int Y { set; get; }

        public override void AddPoint(FontBuilderLine fb)
        {
            fb.To(new Point(X, Y));
        }
    }

    public class AsciiBuilderCopy : AsciiBuilderBase
    {
        [XmlAttribute("Vertical")]
        public bool Vertical { set; get; }

        [XmlAttribute("Flip")]
        public bool Flip { set; get; }

        [XmlAttribute("Src")]
        public int Src { set; get; }

        [XmlAttribute("Dst")]
        public int Dst { set; get; }

        [XmlAttribute("Count")]
        public int Count { set; get; }

        public override void AddPath(FontBuilder fb)
        {
            if (Flip)
            {
                if (Vertical)
                {
                    fb.CopyAndFlipV(Src, Dst, Count);
                }
                else
                {
                    fb.CopyAndFlipH(Src, Dst, Count);
                }
            }
            else
            {
                if (Vertical)
                {
                    fb.CopyV(Src, Dst, Count);
                }
                else
                {
                    fb.CopyH(Src, Dst, Count);
                }
            }
        }
    }
}
