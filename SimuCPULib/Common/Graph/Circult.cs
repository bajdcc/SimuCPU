using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SimuCPULib.Common.Base;
using SimuCPULib.Common.Element;
using SimuCPULib.Common.Simulator;
using SimuCPULib.CPU.Core;
using SimuCPULib.CPU.Test;
using SimuCPULib.GPU.Core;
using SimuCPULib.UI.Global;

namespace SimuCPULib.Common.Graph
{
    /// <summary>
    /// 电路
    /// </summary>
    public class Circult : CircultBase<Status, CommonNode<Status>>, IGPUDisplay
    {
		private MarkableArgs _hover;//悬停参数
		private MarkableArgs _focus;//焦点参数
		private Timer _delay;//悬停时长
		private bool _delayTip;//悬停标志
		private bool _drag;//拖曳标志
        private Rectangle _bound = new Rectangle(Point.Empty, Storage.Size);
        private Machine _machine;
        private CPUContext _ctx;

        public Circult()
		{
			_Init();
		}

		private void _Init()
		{
			if (Storage.Graphics != null)
			{
				_delay = Storage.Delay;
				_delay.Tick += Delay_Tick;
				_delay.Interval = 3000;
				_delay.Enabled = true;
			}
		}

        public void Create()
        {
            _Create();
        }

        private void _Create()
        {
            for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
            {
                for (int i = 0; i < Defines.WIDTH_COUNT; i++)
                {
                    var node = CreateNode();
                    node.Location = new Point(
                        Defines.NODE_OFFSET_X + i*Defines.NODE_WIDTH,
                        Defines.NODE_OFFSET_Y + j*Defines.NODE_HEIGHT);
                    node.RelBound = new Rectangle(
                        node.Location.X,
                        node.Location.Y,
                        Defines.NODE_WIDTH,
                        Defines.NODE_HEIGHT);
                    node.Prepare(_bound);
                }
            }
            Storage.Graphics.Clear(Constants.WindowBackground);
            _machine = new Machine {Display = this};
            Storage.CPUInst = TestInst.Inst;
            _ctx = _machine.Compile(Storage.CPUInst);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        public void Draw()
        {
            var updated = Nodes.Values.Any(a => a.Active);
            foreach (var node in Nodes.Values.Where(a => a.Active))
            {
                node.Update();
                node.Draw(_bound);
                node.Active = false;
            }
            if (updated)
                Storage.Ctrl.Refresh();
        }
        
		/// <summary>
		/// 查找对象
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		private MarkableArgs _FindMarkable(Point pt)
		{
			var args = new MarkableArgs { Pt = pt };
			return Nodes.Values.Any(node => node.Handle(HandleType.Test, args) == 0) ? args : null;
		}

		private void Delay_Tick(object sender, EventArgs e)
		{
		    _hover?.Draw.Handle(HandleType.Hover, Storage.MousePosition);
		    _delayTip = false;
			_delay.Stop();
		}

		/// <summary>
		/// GPU
		/// </summary>
		public void OnGPUTimer()
		{
			Draw();
		}

        /// <summary>
		/// CPU
		/// </summary>
		public void OnCPUTimer()
        {
            Update();
        }

        private void Update()
        {
            _machine.Step(_ctx);
        }

        private void DoFocus(MarkableArgs obj, Point pt)
		{
			_focus = obj;
			_focus.Draw.Handle(HandleType.Enter, pt);
			_drag = true;
		}

		private void DoLostFocus(Point pt)
		{
			_focus.Draw.Handle(HandleType.Leave, pt);
			_focus = null;
			_drag = false;
		}

		public void OnMouseDown(MouseEventArgs e)
		{
			var obj = _FindMarkable(e.Location);
			if (_focus != null)
			{
				if (obj != null)
				{
					obj.Draw.Handle(HandleType.LeftDown, e.Location);
				    if (obj.Id.Equals(_focus.Id)) return;
				    DoLostFocus(e.Location);
				    DoFocus(obj, e.Location);
				}
				else
				{
					DoLostFocus(e.Location);
				}
			}
			else
			{
			    if (obj == null) return;
			    obj.Draw.Handle(HandleType.LeftDown, e.Location);
			    DoFocus(obj, e.Location);
			}
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			if (_drag)
			{
				_drag = false;
			}
			var obj = _FindMarkable(e.Location);
			if (obj != null && _focus != null && obj.Id.Equals(_focus.Id))
			{
				obj.Draw.Handle(HandleType.LeftUp, e.Location);
			}
		}

		public void OnMouseMove(MouseEventArgs e)
		{
			var obj = _FindMarkable(e.Location);
			if (_drag)
			{
				if (obj != null && obj.Id.Equals(_focus.Id))
				{
					if (_delayTip)
					{
						_delayTip = false;
						_delay.Stop();
					}
					_focus.Draw.Handle(HandleType.Drag, Point.Subtract(e.Location, (Size)_focus.Pt));
					_focus.Pt = e.Location;
				}
				else
				{
					_drag = false;
					_focus = null;
				}
				return;
			}
			if (_hover != null)
			{
				if (obj != null)
				{
					if (!obj.Id.Equals(_hover.Id))
					{
						DoLeave(e.Location);
						DoEnter(obj, e.Location);
					}
				}
				else
				{
					DoLeave(e.Location);
				}
			}
			else
			{
				if (obj != null)
				{
					DoEnter(obj, e.Location);
				}
			}
		}

		private void DoEnter(MarkableArgs obj, Point pt)
		{
			_hover = obj;
			_hover.Draw.Handle(HandleType.Enter, pt);
			_hover.Draw.Handle(HandleType.Move, pt);
			if (!_delayTip)
			{
				_delayTip = true;
				_delay.Start();
			}
		}

		private void DoLeave(Point pt)
		{
			_drag = false;
			_hover.Draw.Handle(HandleType.Leave, pt);
			_hover = null;
			if (_delayTip)
			{
				_delayTip = false;
				_delay.Stop();
			}
		}

		public void OnMouseEnter(Point pt)
		{
			var obj = _FindMarkable(pt);
			if (obj != null)
			{
				DoEnter(obj, pt);
			}
		}

		public void OnMouseLeave(Point pt)
		{
			if (_hover != null)
			{
				DoLeave(pt);
			}
		}

		public void OnMouseHover(Point pt)
		{
		    _hover?.Draw.Handle(HandleType.Hover, pt);
		}

        public void SetPixel(Point point, int color)
        {
            var node = Nodes[GetGuidById(point.X*Defines.WIDTH_COUNT + point.Y)];
            node.Next.Color = color;
            node.Update();
        }

        public Size GetFontSize()
        {
            return GPUDefines.FontSize;
        }

        public Size GetScreenSize()
        {
            return new Size(Defines.WIDTH_COUNT, Defines.HEIGHT_COUNT);
        }

        public Color GetFgColor()
        {
            return Color.White;
        }

        public Color GetBkColor()
        {
            return Color.Black;
        }
    }
}
