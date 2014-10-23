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
    public partial class NewFileForm : Form
    {
        public Bitmap Image;
        public string ImageName;
        public string ImageType;
        public NewFileForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ImageName = textBox1.Text;
            Image = new Bitmap(Int16.Parse(textBox2.Text), Int16.Parse(textBox3.Text));
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
