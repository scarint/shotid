namespace ImageAnalysis
{
    partial class Form1
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
            this.OpenButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.CompareButton = new System.Windows.Forms.Button();
            this.cropButton = new System.Windows.Forms.Button();
            this.SetBaseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(12, 12);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(15, 50);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1296, 864);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_Click);
            // 
            // CompareButton
            // 
            this.CompareButton.Location = new System.Drawing.Point(1442, 79);
            this.CompareButton.Name = "CompareButton";
            this.CompareButton.Size = new System.Drawing.Size(75, 23);
            this.CompareButton.TabIndex = 4;
            this.CompareButton.Text = "Compare";
            this.CompareButton.UseVisualStyleBackColor = true;
            this.CompareButton.Visible = false;
            this.CompareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // cropButton
            // 
            this.cropButton.Location = new System.Drawing.Point(1342, 50);
            this.cropButton.Name = "cropButton";
            this.cropButton.Size = new System.Drawing.Size(75, 23);
            this.cropButton.TabIndex = 14;
            this.cropButton.Text = "Crop";
            this.cropButton.UseVisualStyleBackColor = true;
            this.cropButton.Visible = false;
            this.cropButton.Click += new System.EventHandler(this.CropButton_Click);
            // 
            // SetBaseButton
            // 
            this.SetBaseButton.Location = new System.Drawing.Point(1442, 50);
            this.SetBaseButton.Name = "SetBaseButton";
            this.SetBaseButton.Size = new System.Drawing.Size(75, 23);
            this.SetBaseButton.TabIndex = 15;
            this.SetBaseButton.Text = "Set Base";
            this.SetBaseButton.UseVisualStyleBackColor = true;
            this.SetBaseButton.Click += new System.EventHandler(this.SetBaseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 1173);
            this.Controls.Add(this.SetBaseButton);
            this.Controls.Add(this.cropButton);
            this.Controls.Add(this.CompareButton);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.OpenButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button CompareButton;
        private System.Windows.Forms.Button cropButton;
        private System.Windows.Forms.PictureBox[] thumbnail = new System.Windows.Forms.PictureBox[15];
        private System.Windows.Forms.Button SetBaseButton;
    }
}

