using System.Collections.Generic;
using System.Drawing;
using SimuCPULib.Common.Base;
using SimuCPULib.Common.Drawing;
using SimuCPULib.Common.Graph;
using SimuCPULib.Common.Simulator;
using SimuCPULib.UI.Drawing;
using SimuCPULib.UI.Element;
using SimuCPULib.UI.Global;

namespace SimuCPULib.Common.Element
{
    public abstract class NodeX<T> : Node<T>, IDraw
        where T : Status, new()
    {
        public NodeX()
        {
            _relBound.Size = new Size(Defines.NODE_SMALL, Defines.NODE_SMALL);
            _L2_background = BackgroundElement.Create();
            _L2_background[GraphicsDefines.Background_Color] = Color.FromArgb(Local.Color);
            _elements.Add(_L2_background);
            OnStateUpdated += NodeX_OnStateUpdated;
            OnValueUpdated += NodeX_OnValueUpdated;
        }

        private Rectangle _absBound = Rectangle.Empty;

        public Rectangle AbsBound
        {
            get { return _absBound; }
            set { _absBound = value; }
        }

        private Rectangle _relBound = Rectangle.Empty;
        public Rectangle RelBound
        {
            get { return _relBound; }
            set { _relBound = value; }
        }

        public Point Location
        {
            get { return _relBound.Location; }
            set { _relBound.Location = value; }
        }

        private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

        public List<IGraphicsElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
        
        protected BackgroundElement _L2_background;

        public virtual void Draw(Rectangle bound)
        {
            foreach (var e in _elements)
            {
                e.GetRenderer().Render(_absBound);
            }
        }

        public virtual void Prepare(Rectangle bound)
        {
            _absBound = bound.AdjustBound(_relBound);
            _L2_background[GraphicsDefines.Gdi_Bound] = _absBound;
        }

        protected virtual void NodeX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
        {

        }

        protected virtual void NodeX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
        {
            _L2_background[GraphicsDefines.Background_Color] = Color.FromArgb(Local.Color);
        }

        public virtual int Handle(HandleType type, object obj)
        {
            if (type == HandleType.Test)
            {
                var args = obj as MarkableArgs;
                if (_absBound.Contains(args.Pt))
                {
                    args.Id = this;
                    args.Draw = this;
                    return 0;
                }
                return -1;
            }
            var pt = (Point)obj;
            var ret = 0;
            switch (type)
            {
                case HandleType.LeftUp:
                    ret = _Click(pt);
                    break;
                case HandleType.Enter:
                    ret = _Enter(pt);
                    break;
                case HandleType.Leave:
                    ret = _Leave(pt);
                    break;
                case HandleType.Focus:
                    ret = _Focus(pt);
                    break;
                case HandleType.LostFocus:
                    ret = _LostFocus(pt);
                    break;
                case HandleType.Hover:
                    ret = _Hover(pt);
                    break;
                case HandleType.Drag:
                    ret = _Drag(pt);
                    break;
                default:
                    break;
            }
            if (ret == 0)
                return ret;
            return -1;
        }

        protected virtual int _Click(Point pt)
        {
            return 0;
        }

        protected virtual int _Enter(Point pt)
        {
            return 0;
        }

        protected virtual int _Leave(Point pt)
        {
            Storage.Tip.Hide(Storage.Ctrl);
            return 0;
        }

        protected virtual int _Focus(Point pt)
        {
            return 0;
        }
        protected virtual int _LostFocus(Point pt)
        {
            return 0;
        }

        protected virtual int _Hover(Point pt)
        {
            /*StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Type: {0}\n", GetType());
			sb.AppendFormat("Guid: {0}\n", Id);
			sb.AppendFormat("Name: {0}\n", Name);
			sb.AppendFormat("Value: {0}\n", Local.Color);
			Storage.Tip.ToolTipTitle = "Node Infomation";
			Storage.Tip.Show(sb.ToString(), Storage.Ctrl, pt);*/
            return 0;
        }

        protected virtual int _Drag(Point pt)
        {
            return 0;
        }
    }
}
