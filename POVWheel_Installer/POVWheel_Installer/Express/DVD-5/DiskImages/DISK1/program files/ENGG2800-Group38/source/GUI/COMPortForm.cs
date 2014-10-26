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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                    cancelButton.Enabled = false;
                    
                    //Show Progress Bar
                    progressBar1.Show();
                    sendingLabel.Show();

                    //Get Seleted Com
                    string SelectedCom = (string)comboBox1.Items[comboBox1.SelectedIndex];
                    DataAccess.USBCommunication.UploadData(SelectedCom, 19200);
                    //Making fake progress effect
                    for (int i = 0; i < 100; i++)
                    {
                        progressBar1.PerformStep();
                        Thread.Sleep(10);
                    }
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("Upload Successful!");

                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                } 
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
