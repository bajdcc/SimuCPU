using SimuCPULib.Common.Graph;
using SimuCPULib.Common.Simulator;
using SimuCPULib.UI.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimuCPU
{
    public partial class MainForm : Form
    {
        private Circult _circult;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Storage.InitializeGui(new Size(800, 600));
            _circult = new Circult();
            pictureBox1.Image = Storage.Bitmap;
            Storage.Ctrl = pictureBox1;
            Storage.Tip = toolTip1;
            _circult.Create();
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _circult.OnGPUTimer();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            _circult.OnCPUTimer();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _circult.OnMouseDown(e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _circult.OnMouseUp(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _circult.OnMouseMove(e);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            _circult.OnMouseEnter(pictureBox1.PointToClient(MousePosition));
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            _circult.OnMouseLeave(pictureBox1.PointToClient(MousePosition));
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                timer2.Enabled = !timer2.Enabled;
            }
        }
    }
}
