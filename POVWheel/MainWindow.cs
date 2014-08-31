using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POVWheel
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void addLog(string message)
        {
            DateTime saveNow = DateTime.Now;
            listBox1.Items.Add('[' + saveNow.ToString() + "]  " + message);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void sentFileButton_Click(object sender, EventArgs e)
        {

        }

        private void openImageButton_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "PBM Files - Netpbm Portable BitMap (*.pbm)|*.pbm";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    addLog("Open file: " +openDialog.FileName);
                    System.Windows.Media.Imaging.BitmapImage image = new System.Windows.Media.Imaging.BitmapImage();
                    DataAccess.FileHandling.readData(openDialog.FileName, image);
                    pictureBox1.Image = Image.FromFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\Output3.bmp");
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void sendTextButton_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
