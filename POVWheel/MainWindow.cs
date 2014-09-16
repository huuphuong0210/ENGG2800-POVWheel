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
                    System.Drawing.Bitmap image = Program.openImage(FilePath, ref OriginalW, ref OrginalH);
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

        }
    }
}
