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
        public void SetComPorts(string[] ports){
            comboBox1.Items.AddRange(ports);
        }
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
           // Byte[] image = DataAccess.USBCommunication.GetBytesFromCurrentImage();
            //Console.Write("Width:" + image[1] + "Height" + image[2]);
            //Initialize the serialport class
         try{
                 System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort((string)comboBox1.Items[comboBox1.SelectedIndex], 19200);
                //4 5 3
                char[] c = new char[3] { '3', '4', '5' };
                byte[] image = new byte[32*360];
                sp.Open();
                Random rm = new Random();
                int sleep = 200;
                int i =  0;

                bool delay = false;
                Console.WriteLine("Hi Jeremy, Do you want to set the delay for sending process?");
                Console.WriteLine("Press 1 for Yes or 0 for No! then hit enter");
                string keyCode = Console.ReadLine();
                char key = keyCode[0];
                if (key == '1')
                {
                    delay = true;
                    Console.WriteLine("Enter delay time (ms):");
                    string delayTime = Console.ReadLine();
                    sleep = Int32.Parse(delayTime);
                }


                image = DataAccess.USBCommunication.GetBytesFromCurrentImage();

                if (delay)
                {
                    for (i = 0; i < 32 * 360; i++)
                    {
                        if ( image[i] == 255 ) Console.WriteLine("Byte number: " + i + " Value: " + image[i]);
                        sp.Write(image, i, 1);
                        Thread.Sleep(sleep);
                    }
                }
                else
                {
                    sp.Write(image, 0, 32 * 360);
                    Console.Write("Sucessfull 32*360 bytes was sent");
                }


         }catch(Exception ee){
             Console.WriteLine("ERROR: " + ee.Message);
             Console.WriteLine(ee.StackTrace);
             Console.WriteLine("Close and try again!");
         }   
            
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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
