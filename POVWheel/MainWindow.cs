using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
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
            
            OpenFileDialog openDialog = new OpenFileDialog();
            //openDialog.Filter = "PBM Files (*.pbm)|*.pbm | PGM Files (*.pgm) |*.pgm";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Get Image From File
                    System.Drawing.Bitmap image = Program.openImage(openDialog.FileName);
                    pictureBox1.Image = Program.resizeBitmap(image,980,87);

                    //Display Preview Image
                    System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, 290, 290);
                    pictureBox2.Image = previewImage;
    
                    addLog("Openned file: " + openDialog.SafeFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                   addLog("Error: " + ex.Message);
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //using (Font arialFont = new Font("Arial", 10))
            //{
            //    g.DrawString("HEHEHEH", arialFont, Brushes.Blue, new PointF(10f, 10f));
            //    //g.DrawString("HIHIHIHI", arialFont, Brushes.Red, new PointF(10f, 10f));
            //}
            //int numOfCells = 200;
            //int cellSize = 10;
            //Pen p = new Pen(System.Drawing.ColorTranslator.FromHtml("#eaeff2"));

            //for (int y = 0; y < numOfCells; ++y)
            //{
            //    g.DrawLine(p, 0, y * cellSize, numOfCells * cellSize, y * cellSize);
            //}

            //for (int x = 0; x < numOfCells; ++x)
            //{
            //    g.DrawLine(p, x * cellSize, 0, x * cellSize, numOfCells * cellSize);
            //}
        }

        private void renderButton_Click(object sender, EventArgs e)
        {
            renderButton.Enabled = false;
            String textInput = textBox1.Text;
            //textBox1.Clear();
            System.Drawing.Bitmap image = Program.renderImageFromText(textInput);
            Console.WriteLine("Rendering Image Width: " + image.Width + " Heigh: " + image.Height);
            pictureBox1.Image = Program.resizeBitmap(image, 980, 87);

            //Display Preview Image
            System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, 290, 290);
            pictureBox2.Image = previewImage;

            addLog("Render Sucessfully: " + textInput);
            renderButton.Enabled = true;
           

        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Bitmap image = Program.getWheelPreview(Program.resizeBitmap(Program.renderImageFromText(textBox1.Text), 360, 32), 290, 290);
                Console.WriteLine("Rendering Image Width: " + image.Width + " Heigh: " + image.Height);
                pictureBox2.Image = image;
                //pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                addLog("Error: " + ex.Message);
            }
        }
    }
}
