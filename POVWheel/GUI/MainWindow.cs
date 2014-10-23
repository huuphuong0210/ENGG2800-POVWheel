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
namespace POVWheel.GUI
{
    public partial class MainWindow : Form
    {
        private bool Pencil = false;
        private System.Drawing.Color Color = System.Drawing.Color.Black;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddFileInfor(string fileName, string fileType, string fileWidth, string fileHeight)
        {
            FileNameLabel.Text = fileName;
            FileTypeLabel.Text = fileType;
            FileWidthLabel.Text = "W: " + fileWidth +"px";
            FileHeightLabel.Text = "H: " + fileHeight + "px";
        }

        public void ClearFileInfor()
        {
            FileNameLabel.Text = " ";
            FileTypeLabel.Text = " ";
            FileWidthLabel.Text = " ";
            FileHeightLabel.Text = " ";
        }
        public void addLog(string message)
        {
            DateTime saveNow = DateTime.Now;
            listBox1.Items.Add('[' + saveNow.ToString() + "]  " + message);
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            COMPortForm testDialog = new COMPortForm();
            testDialog.SetComPorts(System.IO.Ports.SerialPort.GetPortNames());
            testDialog.StartPosition = FormStartPosition.CenterParent;
           
            // Show testDialog as a modal dialog and determine if DialogResult = OK. 
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox. 
                //this.txtResult.Text = testDialog.TextBox1.Text;
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Cancel");
                //this.txtResult.Text = "Cancelled";
            }
            testDialog.Dispose();
        }

        private void openImageButton_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openDialog = new OpenFileDialog();
            //openDialog.Filter = "PBM Files (*.pbm)|*.pbm | PGM Files (*.pgm) |*.pgm";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String FilePath = openDialog.FileName;
                    //Get Image From File
                    int OriginalW = 0;
                    int OrginalH = 0;
                    System.Drawing.Bitmap image = Program.OpenImage(FilePath, ref OriginalW, ref OrginalH);
                    pictureBox1.Image = Program.resizeBitmap(image,976,87);

                    //Display Preview Image
                    System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
                    pictureBox2.Image = previewImage;
                    
                    addLog("Openned file: " + openDialog.SafeFileName);
                    string FileName = Path.GetFileNameWithoutExtension(FilePath);
                    string FileType = Path.GetExtension(FilePath);
                    string FileWidth = OriginalW.ToString();
                    string FileHeight = OrginalH.ToString();

                    AddFileInfor(FileName, FileType, FileWidth, FileHeight);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                   addLog("Error: " + ex.Message);
                }
            }
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
            pictureBox1.Image = Program.resizeBitmap(image, 976, 87);

            //Display Preview Image
            System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Image = previewImage;

            addLog("Render Sucessfully: " + textInput);
            renderButton.Enabled = true;

            //Dipslay image RGB value
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Console.WriteLine("R: " + image.GetPixel(x, y).R
                        + " B: " + image.GetPixel(x, y).B
                        + " G: " + image.GetPixel(x, y).G);
                }
            } 
           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FileHeightLabel_Click(object sender, EventArgs e)
        {

        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            NewFileForm NewFileDialog = new NewFileForm();
            NewFileDialog.StartPosition = FormStartPosition.CenterParent;

            // Show testDialog as a modal dialog and determine if DialogResult = OK. 
            if (NewFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox. 
                //this.txtResult.Text = testDialog.TextBox1.Text;
                Console.WriteLine("OK");
                pictureBox1.Image = new Bitmap(976, 87);
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.Clear(Color.White);
                AddFileInfor(NewFileDialog.ImageName, " ", NewFileDialog.Image.Width.ToString(), NewFileDialog.Image.Height.ToString());
            }
            else
            {
                Console.WriteLine("Cancel");
                //this.txtResult.Text = "Cancelled";
            }           
            //pictureBox1.Image = NewFileDialog.Image;
           
            NewFileDialog.Dispose();

        }

        private bool mouseDown = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Bitmap image = (Bitmap)pictureBox1.Image;
                Graphics g = Graphics.FromImage(image);
                if (e.X >= 0 && e.Y >= 0)
                {
                    float x,y;

                    if (e.X >= 1) x = e.X - 1;
                    else x = e.X;
                    y = e.Y + 1;
                    //image.SetPixel(e.X, e.Y, Color.Black);
                    //image.SetPixel(e.X, e.Y, Color.Black);
                    g.FillRectangle(new SolidBrush(Color), x, y, (float)2.7, (float)2.7);
                    //Console.WriteLine("X: " + e.X + " Y: " + e.Y);
                    pictureBox1.Image = image;
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog ColorChoser = new ColorDialog();
            if (ColorChoser.ShowDialog(this) == DialogResult.OK)
            {
                Color = ColorChoser.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap image = (Bitmap)pictureBox1.Image;
            image = Program.resizeBitmap(image, 360, 32);
            System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Image = previewImage;

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
           
        }
    }
}
