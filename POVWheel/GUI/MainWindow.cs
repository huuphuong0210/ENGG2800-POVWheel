﻿using System;
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
        public int CurrentCursor = 2; // 1-pointer | 2-brushes
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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FileHeightLabel_Click(object sender, EventArgs e)
        {

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

                //Graphics graphicOrginialImage = Graphics.FromImage(Program.CurrentImage);
                //if (e.X >= 0 && e.Y >= 0)
                //{
                //    float x,y;

                //    if (e.X >= 1) x = e.X - 1;
                //    else x = e.X;

                //    y = e.Y + 1;
                //    //image.SetPixel(e.X, e.Y, Color.Black);
                //    //image.SetPixel(e.X, e.Y, Color.Black);
                //    g.FillRectangle(new SolidBrush(Color), x, y, (float)2.7, (float)2.7);
                //    //Console.WriteLine("X: " + e.X + " Y: " + e.Y);
                //    pictureBox1.Image = image;
                //}

                if (e.X >= 0 && e.Y >= 0)
                {
                    //Console.WriteLine("["+e.X+","+e.Y+"]");
                    int haftScaleWidth = Convert.ToInt32(Math.Floor(Program.CurrentImage.Width / 2 * 2.71));
                    int xLowerBound = 488 - haftScaleWidth;
                    int xUpperBound = 488 + haftScaleWidth;

                    int haftScaleHeight= Convert.ToInt32(Math.Floor(Program.CurrentImage.Height/2*2.71));
                    int yLowerBound = 43 - haftScaleHeight;
                    int yUpperBound = 43 + haftScaleHeight;

                    int mapBackX = Convert.ToInt32(Math.Floor((e.X - xLowerBound)/2.71));
                    int mapBackY = Convert.ToInt32(Math.Floor((e.Y - yLowerBound) / 2.71));

                    if (e.X < xLowerBound || e.X > xUpperBound || e.Y < yLowerBound || e.Y > yUpperBound) return;

                    Console.WriteLine("[" + mapBackX + "," + mapBackY + "]");

                    float x, y;

                    //if (e.X >= 1) x = e.X - 1;
                    //else x = e.X;

                    //y = e.Y + 1;
                    //image.SetPixel(e.X, e.Y, Color.Black);
                    //image.SetPixel(e.X, e.Y, Color.Black);
                    //g.FillRectangle(new SolidBrush(Color), x, y, (float)2.7, (float)2.7);
                    //Console.WriteLine("X: " + e.X + " Y: " + e.Y);
                    Color myRgbColor = new Color();
                    myRgbColor = Color.FromArgb(0, 0, 0);
                    Program.CurrentImage.SetPixel(mapBackX, mapBackY, myRgbColor);

                    System.Drawing.Bitmap image2 = Program.GetImageForDipslay(Program.CurrentImage);
                    pictureBox1.Image = Program.resizeBitmap(image2, 976, 87);

                    //Display Preview Image
                    //System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
                    //pictureBox2.Image = previewImage;
                    //pictureBox1.Image = image;
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFileForm NewFileDialog = new NewFileForm();

            NewFileDialog.StartPosition = FormStartPosition.CenterParent;

            // Show testDialog as a modal dialog and determine if DialogResult = OK. 
            if (NewFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox. 
                //this.txtResult.Text = testDialog.TextBox1.Text;
                Console.WriteLine("OK");

                Bitmap image = Program.CreateNewImage(NewFileDialog.ImageType, NewFileDialog.Width, NewFileDialog.Heigh, NewFileDialog.ImageName);
                //AddFileInfor(NewFileDialog.ImageName, " ", NewFileDialog.Image.Width.ToString(), NewFileDialog.Image.Height.ToString());
                
                pictureBox1.Image = Program.resizeBitmap(image, 976, 87);

                //Display Preview Image
                System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
                pictureBox2.Image = previewImage;

                addLog("Create new file: " + NewFileDialog.ImageName);
                string FileName = NewFileDialog.ImageName;
                string FileType = "";
                string FileWidth = Program.CurrentImage.Width.ToString();
                string FileHeight = Program.CurrentImage.Width.ToString();

                AddFileInfor(FileName, FileType, FileWidth, FileHeight);

                //Enable Save Menu
                saveToolStripMenuItem.Enabled = true;

                //Enable Draw Tools
                toolStrip1.Enabled = true;
            }
            else
            {
                Console.WriteLine("Cancel");
            }
            NewFileDialog.Dispose();
            
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                //openDialog.Filter = "PBM Files (*.pbm)|*.pbm | PGM Files (*.pgm) |*.pgm";

                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                        String FilePath = OpenDialog.FileName;
                        //Get Image From File
                        int OriginalW = 0;
                        int OrginalH = 0;
                        System.Drawing.Bitmap image = Program.OpenImage(FilePath, ref OriginalW, ref OrginalH);
                        pictureBox1.Image = Program.resizeBitmap(image, 976, 87);

                        //Display Preview Image
                        System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
                        pictureBox2.Image = previewImage;

                        addLog("Openned file: " + OpenDialog.SafeFileName);
                        string FileName = Path.GetFileNameWithoutExtension(FilePath);
                        string FileType = Path.GetExtension(FilePath);
                        string FileWidth = OriginalW.ToString();
                        string FileHeight = OrginalH.ToString();

                        AddFileInfor(FileName, FileType, FileWidth, FileHeight);

                        //Enable Save File Menu
                        saveToolStripMenuItem.Enabled = true;

                        //Enable Draw Tools
                        toolStrip1.Enabled = true;
                        
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                addLog("Error: " + exeption.Message);
            }
            
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void renderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TextRenderForm renderForm = new TextRenderForm();
                renderForm.StartPosition = FormStartPosition.CenterParent;

                // Show testDialog as a modal dialog and determine if DialogResult = OK. 
                if (renderForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Read the contents of testDialog's TextBox. 
                    //this.txtResult.Text = testDialog.TextBox1.Text;
                    //Console.WriteLine("OK");

                    String textInput = renderForm.Input;
                    //Render Image from Text
                    System.Drawing.Bitmap image = Program.renderImageFromText(textInput);
                    //Console.WriteLine("Rendering Image Width: " + image.Width + " Heigh: " + image.Height);
                    pictureBox1.Image = Program.resizeBitmap(image, 976, 87);

                    //Display Preview Image
                    System.Drawing.Bitmap previewImage = Program.getWheelPreview(image, pictureBox2.Width, pictureBox2.Height);
                    pictureBox2.Image = previewImage;

                    addLog("Render Sucessfully: " + textInput);
                    
                    //Enable Save Menu
                    saveToolStripMenuItem.Enabled = true;

                    //Enable Draw Tools
                    toolStrip1.Enabled = true;
                }
                else
                {
                    Console.WriteLine("Cancel");
                    //this.txtResult.Text = "Cancelled";
                }
                renderForm.Dispose();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (Program.ImageType == 1) // Black-White Image
                    saveFileDialog.Filter = "PBM File |*.pbm";
                else if (Program.ImageType == 2) // Gray-scale Image
                    saveFileDialog.Filter = "PGM File |*.pgm";
                else if (Program.ImageType == 3) // Color Image
                    saveFileDialog.Filter = "PPM File |*.ppm";

                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    DataAccess.FileHandling.SavingImage(saveFileDialog.FileName, Program.ImageType, Program.CurrentImage);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void uploadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.CurrentImage == null)
                {
                    MessageBox.Show("Please open image or render the text first!");
                    return;
                }

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
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void toolStripBrushButton_Click(object sender, EventArgs e)
        {
            //Set Current Cursor to Brush
            CurrentCursor = 2;
        }

        private void toolStripPointerButton_Click(object sender, EventArgs e)
        {
            //Set Current Cursor to Pointer
            CurrentCursor = 1;
        }
    }
}
