namespace POVWheel.GUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NewFileButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.openImageButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.renderButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.FileTypeLabel = new System.Windows.Forms.Label();
            this.FileWidthLabel = new System.Windows.Forms.Label();
            this.FileHeightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // NewFileButton
            // 
            this.NewFileButton.Location = new System.Drawing.Point(110, 8);
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(84, 34);
            this.NewFileButton.TabIndex = 1;
            this.NewFileButton.Text = "New File";
            this.NewFileButton.UseVisualStyleBackColor = true;
            this.NewFileButton.Click += new System.EventHandler(this.NewFileButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(597, 8);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(84, 34);
            this.UploadButton.TabIndex = 2;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseMnemonic = false;
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // openImageButton
            // 
            this.openImageButton.Location = new System.Drawing.Point(7, 8);
            this.openImageButton.Name = "openImageButton";
            this.openImageButton.Size = new System.Drawing.Size(97, 34);
            this.openImageButton.TabIndex = 3;
            this.openImageButton.Text = "Open Image";
            this.openImageButton.UseVisualStyleBackColor = true;
            this.openImageButton.Click += new System.EventHandler(this.openImageButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.White;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Items.AddRange(new object[] {
            "Starting Window",
            "Hardware component is not connected!"});
            this.listBox1.Location = new System.Drawing.Point(395, 292);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(593, 270);
            this.listBox1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 89);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(976, 87);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // renderButton
            // 
            this.renderButton.Location = new System.Drawing.Point(507, 8);
            this.renderButton.Name = "renderButton";
            this.renderButton.Size = new System.Drawing.Size(84, 34);
            this.renderButton.TabIndex = 6;
            this.renderButton.Text = "Render";
            this.renderButton.UseVisualStyleBackColor = true;
            this.renderButton.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(203, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 32);
            this.textBox1.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(14, 213);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(350, 350);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.FileNameLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameLabel.ForeColor = System.Drawing.Color.White;
            this.FileNameLabel.Location = new System.Drawing.Point(788, 72);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(200, 14);
            this.FileNameLabel.TabIndex = 9;
            this.FileNameLabel.Text = "FileName";
            this.FileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FileNameLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // FileTypeLabel
            // 
            this.FileTypeLabel.AutoSize = true;
            this.FileTypeLabel.BackColor = System.Drawing.Color.Transparent;
            this.FileTypeLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileTypeLabel.ForeColor = System.Drawing.Color.White;
            this.FileTypeLabel.Location = new System.Drawing.Point(21, 218);
            this.FileTypeLabel.Name = "FileTypeLabel";
            this.FileTypeLabel.Size = new System.Drawing.Size(35, 14);
            this.FileTypeLabel.TabIndex = 10;
            this.FileTypeLabel.Text = "Type";
            // 
            // FileWidthLabel
            // 
            this.FileWidthLabel.AutoSize = true;
            this.FileWidthLabel.BackColor = System.Drawing.Color.Transparent;
            this.FileWidthLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileWidthLabel.ForeColor = System.Drawing.Color.White;
            this.FileWidthLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FileWidthLabel.Location = new System.Drawing.Point(311, 218);
            this.FileWidthLabel.Name = "FileWidthLabel";
            this.FileWidthLabel.Size = new System.Drawing.Size(42, 15);
            this.FileWidthLabel.TabIndex = 11;
            this.FileWidthLabel.Text = "Width";
            this.FileWidthLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FileHeightLabel
            // 
            this.FileHeightLabel.AutoSize = true;
            this.FileHeightLabel.BackColor = System.Drawing.Color.Transparent;
            this.FileHeightLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileHeightLabel.ForeColor = System.Drawing.Color.White;
            this.FileHeightLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FileHeightLabel.Location = new System.Drawing.Point(311, 233);
            this.FileHeightLabel.Name = "FileHeightLabel";
            this.FileHeightLabel.Size = new System.Drawing.Size(49, 15);
            this.FileHeightLabel.TabIndex = 12;
            this.FileHeightLabel.Text = "Height";
            this.FileHeightLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FileHeightLabel.Click += new System.EventHandler(this.FileHeightLabel_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::POVWheel.Properties.Resources.App_BackGround;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.FileHeightLabel);
            this.Controls.Add(this.FileWidthLabel);
            this.Controls.Add(this.FileTypeLabel);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.renderButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.openImageButton);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.NewFileButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "POV Wheel - Group 38";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NewFileButton;
        private System.Windows.Forms.Button UploadButton;
        private System.Windows.Forms.Button openImageButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button renderButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label FileTypeLabel;
        private System.Windows.Forms.Label FileWidthLabel;
        private System.Windows.Forms.Label FileHeightLabel;

    }
}

