using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POVWheel.GUI
{
    public partial class NewFileForm : Form
    {
        public string ImageName;
        public int ImageType;
        public int Width;
        public int Heigh;
        public bool IsValid()
        {
            if (nameTextBox.Text.Equals("")) // Empty name field
            {
                errorLabel.Text = "Empty File Name";
                return false;
            }

            int Width;
            if (!Int32.TryParse(widthTextBox.Text, out Width)) // Empty or invalid width field
            {
                errorLabel.Text = "Invalid Width Size";
                return false;
            }

            int Heigh;
            if (!Int32.TryParse(heighTextBox.Text, out Heigh)) // // Empty or invalid width field
            {
                errorLabel.Text = "Invalid Heigh Value";
                return false;
            }

            if (Width > 360 || Width < 1) // Invaid size
            {
                errorLabel.Text = "Invaid Width Size";
                return false;
            }

            if (Heigh > 32 || Heigh < 1) // Invaid size
            {
                errorLabel.Text = "Invalid Heigh Size";
                return false;
            }

            if (imageTypeComboBox.SelectedIndex < 0) 
            {
                errorLabel.Text = "Please select image type!";
                return false;
            }

            return true;
        }
        public NewFileForm()
        {
            InitializeComponent();
        }
    


        private void okButton_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                this.DialogResult = DialogResult.OK;
                ImageName = nameTextBox.Text;
                ImageType = imageTypeComboBox.SelectedIndex + 1; 
                Width = Int16.Parse(widthTextBox.Text);
                Heigh = Int16.Parse(heighTextBox.Text);
                this.Hide();
            }
            
        }

        

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void NewFileForm_Load(object sender, EventArgs e)
        {

        }

        private void imageTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
