namespace TC37852369.UI
{
    partial class ScanningWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanningWindow));
            this.PictureBox_Barcode = new System.Windows.Forms.PictureBox();
            this.Timer_Gif = new System.Windows.Forms.Timer(this.components);
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Timer_Barcode = new System.Windows.Forms.Timer(this.components);
            this.TextBox_Barcode = new System.Windows.Forms.TextBox();
            this.Label_Scanning = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Barcode)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_Barcode
            // 
            this.PictureBox_Barcode.Location = new System.Drawing.Point(112, 84);
            this.PictureBox_Barcode.Name = "PictureBox_Barcode";
            this.PictureBox_Barcode.Size = new System.Drawing.Size(264, 132);
            this.PictureBox_Barcode.TabIndex = 1;
            this.PictureBox_Barcode.TabStop = false;
            // 
            // Timer_Gif
            // 
            this.Timer_Gif.Enabled = true;
            this.Timer_Gif.Interval = 20;
            this.Timer_Gif.Tick += new System.EventHandler(this.Timer_Gif_Tick);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.White;
            this.Button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Cancel.Font = new System.Drawing.Font("Segoe UI Semibold", 7.865546F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Button_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button_Cancel.Location = new System.Drawing.Point(172, 234);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(136, 56);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = false;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Timer_Barcode
            // 
            this.Timer_Barcode.Enabled = true;
            this.Timer_Barcode.Interval = 400;
            this.Timer_Barcode.Tick += new System.EventHandler(this.Timer_Barcode_Tick);
            // 
            // TextBox_Barcode
            // 
            this.TextBox_Barcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_Barcode.ForeColor = System.Drawing.Color.Transparent;
            this.TextBox_Barcode.Location = new System.Drawing.Point(226, 171);
            this.TextBox_Barcode.Name = "TextBox_Barcode";
            this.TextBox_Barcode.Size = new System.Drawing.Size(100, 15);
            this.TextBox_Barcode.TabIndex = 5;
            // 
            // Label_Scanning
            // 
            this.Label_Scanning.AutoSize = true;
            this.Label_Scanning.Font = new System.Drawing.Font("Segoe UI Semibold", 7.260504F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Label_Scanning.Location = new System.Drawing.Point(210, 297);
            this.Label_Scanning.Name = "Label_Scanning";
            this.Label_Scanning.Size = new System.Drawing.Size(0, 15);
            this.Label_Scanning.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Light", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label12.Location = new System.Drawing.Point(23, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 32);
            this.label12.TabIndex = 121;
            this.label12.Text = "Scan barcode";
            // 
            // ScanningWindow
            // 
            this.ApplyImageInvert = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(119F, 119F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(497, 326);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackImage = ((System.Drawing.Image)(resources.GetObject("$this.BackImage")));
            this.ClientSize = new System.Drawing.Size(497, 326);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Label_Scanning);
            this.Controls.Add(this.TextBox_Barcode);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.PictureBox_Barcode);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ScanningWindow";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Barcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PictureBox_Barcode;
        private System.Windows.Forms.Timer Timer_Gif;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Timer Timer_Barcode;
        private System.Windows.Forms.TextBox TextBox_Barcode;
        private System.Windows.Forms.Label Label_Scanning;
        private System.Windows.Forms.Label label12;
    }
}