using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POVWheel.GUI
{
    public partial class GrayScaleColorChoser : Form
    {
        public Color Color;
        public GrayScaleColorChoser()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            int GrayScaleValue = trackBar1.Value;
            Color = Color.FromArgb(GrayScaleValue, GrayScaleValue, GrayScaleValue); ;
            label3.Text = "Current Value: " + GrayScaleValue;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
