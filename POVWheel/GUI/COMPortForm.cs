using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace POVWheel.GUI
{
    public partial class COMPortForm : Form
    {
        public COMPortForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            //Thread readThread = new Thread();
            //List the coms port           
           
            //Initialize the serialport class
            //System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort((string)comboBox1.Items[comboBox1.SelectedIndex], 19200);
            ////4 5 3
            //char[] c = new char[3] { '3', '4', '5' };
            //sp.Open();
            //Random rm = new Random();
            //int sleep = 200;
            //while (true)
            //{
            //    Console.WriteLine("Sequential Display");
            //    for (int k = 0; k < 20; k++)
            //    {
            //        //light up
            //        for (int i = 0; i < 3; i++)
            //        {
            //            sp.Write(c, i, 1);
            //            Thread.Sleep(sleep);
            //        }
            //        //Turn off

            //        //for (int i = 2; i >= 0; i--)
            //        //{
            //        //    sp.Write(c, i, 1);
            //        //    Thread.Sleep(sleep);
            //        //}
            //    }

            //    //Flash three line
            //    Console.WriteLine("Flashing three light");
            //    for (int k = 0; k < 20; k++)
            //    {
            //        sp.Write(c, 0, 3);
            //        Thread.Sleep(sleep);
            //    }
            //    //
            //    sp.Write(c, 0, 1);
            //    sp.Write(c, 2, 1);
            //    for (int k = 0; k < 30; k++)
            //    {
            //        sp.Write(c, 0, 3);
            //        Thread.Sleep(400);
            //    }
            //    sp.Write(c, 0, 1);
            //    sp.Write(c, 2, 1);
            //}


            //Console.Read();
            ///////////////////////////////////////
        }

        private void COMPortForm_Load(object sender, EventArgs e)
        {

        }
    }
}
