namespace POVWheel
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
            this.sentFileButton = new System.Windows.Forms.Button();
            this.sendTextButton = new System.Windows.Forms.Button();
            this.openImageButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // sentFileButton
            // 
            this.sentFileButton.Location = new System.Drawing.Point(110, 8);
            this.sentFileButton.Name = "sentFileButton";
            this.sentFileButton.Size = new System.Drawing.Size(84, 34);
            this.sentFileButton.TabIndex = 1;
            this.sentFileButton.Text = "Sent File";
            this.sentFileButton.UseVisualStyleBackColor = true;
            this.sentFileButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // sendTextButton
            // 
            this.sendTextButton.Location = new System.Drawing.Point(510, 8);
            this.sendTextButton.Name = "sendTextButton";
            this.sendTextButton.Size = new System.Drawing.Size(84, 34);
            this.sendTextButton.TabIndex = 2;
            this.sendTextButton.Text = "Sent Text";
            this.sendTextButton.UseVisualStyleBackColor = true;
            // 
            // openImageButton
            // 
            this.openImageButton.Location = new System.Drawing.Point(7, 8);
            this.openImageButton.Name = "openImageButton";
            this.openImageButton.Size = new System.Drawing.Size(97, 34);
            this.openImageButton.TabIndex = 3;
            this.openImageButton.Text = "Open Image";
            this.openImageButton.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "[hh:mm] Starting Window",
            "Hardware Component Not Connected!"});
            this.listBox1.Location = new System.Drawing.Point(314, 364);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(674, 160);
            this.listBox1.TabIndex = 4;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::POVWheel.Properties.Resources.mainbackgroundimage1;
            this.ClientSize = new System.Drawing.Size(1000, 540);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.openImageButton);
            this.Controls.Add(this.sendTextButton);
            this.Controls.Add(this.sentFileButton);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "POV Whell - Group 38";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sentFileButton;
        private System.Windows.Forms.Button sendTextButton;
        private System.Windows.Forms.Button openImageButton;
        private System.Windows.Forms.ListBox listBox1;

    }
}

