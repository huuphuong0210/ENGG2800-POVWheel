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
            //Combox Item is selected
            if (comboBox1.SelectedIndex > -1) 
            {
                try
                {
                    //Hidden ComboBox & Label 1
                    comboBox1.Hide();
                    label1.Hide();

                    //Disabel OK and Cancel Button
                    button1.Enabled = false;
                    button2.Enabled = false;

                    //Show Progress Bar
                    progressBar1.Show();
                    sendingLabel.Show();
                    //Making fake progress effect
                    for (int i = 0; i < 90; i++)
                    {
                        progressBar1.PerformStep();
                        Thread.Sleep(10);
                    }

                    string SelectedCom = (string)comboBox1.Items[comboBox1.SelectedIndex];
                    DataAccess.USBCommunication.UploadData(SelectedCom, 19200);

                    progressBar1.Increment(10);
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("Upload Successful!");

                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                           
            }
            
        }

        private void COMPortForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void successLabel_Click(object sender, EventArgs e)
        {

        }

    
    }
}
