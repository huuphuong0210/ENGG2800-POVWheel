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
using System.Drawing;
namespace POVWheel.GUI
{
    public partial class MainWindow : Form
    {
        //Current Cursor Type [1-pointer | 2-brushes | 3-erasers] default value is pointer
        public int CurrentCursor = 1;
        //Brush color
        private Color m_BrushColor = Color.Black;
        private bool m_MouseDown = false;

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

        public void AddLog(string message)
        {
            DateTime saveNow = DateTime.Now;
            listBox1.Items.Add('[' + saveNow.ToString() + "]  " + message);
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Setting the mouse down value to true when mouse is click down
            m_MouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Setting the mouse down value to true when mouse click is up
            m_MouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //Drawing when cursor is brush or pointer
                if (m_MouseDown == true && CurrentCursor != 1)
                {
                    //Detect the location of mouse'pointer over the picture box
                    if (e.X >= 0 && e.Y >= 0)
                    {
                        //Mapping pointer coordinate to image pixel coordinate
                        int haftScaleWidth = Convert.ToInt32(Math.Floor(Program.CurrentImage.Width / 2 * 2.71));
                        int xLowerBound = 488 - haftScaleWidth;
                        int xUpperBound = 488 + haftScaleWidth;

                        int haftScaleHeight = Convert.ToInt32(Math.Floor(Program.CurrentImage.Height / 2 * 2.71));
                        int yLowerBound = 43 - haftScaleHeight;
                        int yUpperBound = 43 + haftScaleHeight;

                        int mapBackX = Convert.ToInt32(Math.Floor((e.X - xLowerBound) / 2.71));
                        int mapBackY = Convert.ToInt32(Math.Floor((e.Y - yLowerBound) / 2.71));

                        if (e.X < xLowerBound || e.X > xUpperBound || e.Y < yLowerBound || e.Y > yUpperBound) return;

                        //Drawing on the image
                        if (CurrentCursor == 3) //Eraser
                        {
                            Program.CurrentImage.SetPixel(mapBackX, mapBackY, System.Drawing.Color.White);
                        }
                        else //Brush
                        {
                            Program.CurrentImage.SetPixel(mapBackX, mapBackY, m_BrushColor);
                        }

                        //Rerendering image for dipslay
                        System.Drawing.Bitmap image2 = Program.GetImageToDipslay(Program.CurrentImage);
                        pictureBox1.Image = Program.ResizeBitmap(image2, 976, 87);

                    }

                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog("Error: " + exeption.Message);
            }
            
        }


        private void refreshPreviewButton_Click(object sender, EventArgs e)
        {
            if (Program.CurrentImage != null)
            {
                try
                {
                    //Get current displayed picture
                    Bitmap image = (Bitmap)pictureBox1.Image;
                    image = Program.ResizeBitmap(image, 360, 32);

                    //Get and dipslay preview image
                    Bitmap previewImage = Program.GetWheelPreviewImage(image, pictureBox2.Width, pictureBox2.Height);
                    pictureBox2.Image = previewImage;
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AddLog("Error: " + exeption.Message);
                }
            }
            
        }

        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewFileForm newFileDialog = new NewFileForm();

                //Center the new file diagle
                newFileDialog.StartPosition = FormStartPosition.CenterParent;

                // Show new file diaglog as a modal dialog and determine if DialogResult = OK 
                if (newFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //Create new bitmap image base on user's input
                    Bitmap image = Program.CreateNewImage(newFileDialog.ImageType, newFileDialog.Width, newFileDialog.Heigh, newFileDialog.ImageName);

                    //Get image to dipslay
                    pictureBox1.Image = Program.ResizeBitmap(image, 976, 87);

                    //Display Preview Image
                    Bitmap previewImage = Program.GetWheelPreviewImage(image, pictureBox2.Width, pictureBox2.Height);
                    pictureBox2.Image = previewImage;

                    //Add log to history list
                    AddLog("Create new file: " + newFileDialog.ImageName);
                    string FileName = newFileDialog.ImageName;
                    string FileType = "";
                    string FileWidth = Program.CurrentImage.Width.ToString();
                    string FileHeight = Program.CurrentImage.Width.ToString();

                    AddFileInfor(FileName, FileType, FileWidth, FileHeight);

                    //Enable Save Menu
                    saveToolStripMenuItem.Enabled = true;

                    //Enable Draw Tools
                    toolStrip1.Enabled = true;
                }
                newFileDialog.Dispose();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog("Error: " + exeption.Message);
            }
           
            
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Initilize open file dialog
                OpenFileDialog openDialog = new OpenFileDialog();

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                        String FilePath = openDialog.FileName;

                        //Get Image From File
                        int OriginalW = 0;
                        int OrginalH = 0;
                        System.Drawing.Bitmap image = Program.OpenImage(FilePath, ref OriginalW, ref OrginalH);
                        pictureBox1.Image = Program.ResizeBitmap(image, 976, 87);

                        //Display Preview Image
                        System.Drawing.Bitmap previewImage = Program.GetWheelPreviewImage(image, pictureBox2.Width, pictureBox2.Height);
                        pictureBox2.Image = previewImage;

                        AddLog("Openned file: " + openDialog.SafeFileName);
                        string FileName = Path.GetFileNameWithoutExtension(FilePath);
                        string FileType = Path.GetExtension(FilePath);
                        string FileWidth = OriginalW.ToString();
                        string FileHeight = OrginalH.ToString();
                        
                        //Add file info
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
                AddLog("Error: " + exeption.Message);
            }
            
        }

        private void renderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Initilize text render dialog
                TextRenderForm renderForm = new TextRenderForm();

                //Center the dialog
                renderForm.StartPosition = FormStartPosition.CenterParent;

                // Show testDialog as a modal dialog and determine if DialogResult = OK. 
                if (renderForm.ShowDialog(this) == DialogResult.OK)
                {
                    //Get text input from user
                    String textInput = renderForm.Input;

                    //Render image from user input and display 
                    System.Drawing.Bitmap image = Program.RenderImageFromText(textInput);
                    pictureBox1.Image = Program.ResizeBitmap(image, 976, 87);

                    //Display preview image
                    System.Drawing.Bitmap previewImage = Program.GetWheelPreviewImage(image, pictureBox2.Width, pictureBox2.Height);
                    pictureBox2.Image = previewImage;
                    
                    //Add Log
                    AddLog("Render Sucessfully: " + textInput);
                    
                    //Enable Save Menu
                    saveToolStripMenuItem.Enabled = true;

                    //Enable Draw Tools
                    toolStrip1.Enabled = true;
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
                //Initialize save file diaglog
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                //Adding file extension filter
                if (Program.CurrentImageType == 1) // Black-White Image
                    saveFileDialog.Filter = "PBM File |*.pbm";
                else if (Program.CurrentImageType == 2) // Gray-scale Image
                    saveFileDialog.Filter = "PGM File |*.pgm";
                else if (Program.CurrentImageType == 3) // Color Image
                    saveFileDialog.Filter = "PPM File |*.ppm";

                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                
                //Saving file
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    DataAccess.FileHandling.SavingImage(saveFileDialog.FileName, Program.CurrentImageType, Program.CurrentImage);
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

                //Initialize the comport chosing diaglog
                COMPortForm comPortDialog = new COMPortForm();
                comPortDialog.SetComPorts(System.IO.Ports.SerialPort.GetPortNames());
                comPortDialog.StartPosition = FormStartPosition.CenterParent;

                // Show comPortDialog as a modal dialog and determine if DialogResult = OK. 
                comPortDialog.ShowDialog(this);
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

            //Disable Brush Button
            toolStripBrushButton.Enabled = false;

            //Enable Pointer and Eraser Button
            toolStripPointerButton.Enabled = true;
            toolStripEraserButton.Enabled = true;
        }

        private void toolStripPointerButton_Click(object sender, EventArgs e)
        {
            //Set Current Cursor to Pointer
            CurrentCursor = 1;

            //Disable Pointer Button
            toolStripPointerButton.Enabled = false;

            //Enable Brush and Eraser Button
            toolStripBrushButton.Enabled = true;
            toolStripEraserButton.Enabled = true;
        }

        private void toolStripEraserButton_Click(object sender, EventArgs e)
        {
            //Set Current Cursor to Eraser
            CurrentCursor = 3;

            //Disable Eraser Button
            toolStripEraserButton.Enabled = false;

            //Enable Brush and Pointer Button
            toolStripBrushButton.Enabled = true;
            toolStripPointerButton.Enabled = true;
        }

        private void toolStripColorPickerButton_Click(object sender, EventArgs e)
        {
            ColorDialog ColorChoser = new ColorDialog();

            if (ColorChoser.ShowDialog(this) == DialogResult.OK)
            {
                m_BrushColor = ColorChoser.Color;
            }
        }
    }
}
