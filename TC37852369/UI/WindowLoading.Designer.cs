namespace TC37852369.UI
{
    partial class WindowLoading
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
            this.PictureBox_LoadingImage = new System.Windows.Forms.PictureBox();
            this.Timer_TicketLoadingGif = new System.Windows.Forms.Timer(this.components);
            this.Label_LoadingData = new System.Windows.Forms.Label();
            this.Timer_Label = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_LoadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_LoadingImage
            // 
            this.PictureBox_LoadingImage.Location = new System.Drawing.Point(187, 63);
            this.PictureBox_LoadingImage.Name = "PictureBox_LoadingImage";
            this.PictureBox_LoadingImage.Size = new System.Drawing.Size(400, 300);
            this.PictureBox_LoadingImage.TabIndex = 0;
            this.PictureBox_LoadingImage.TabStop = false;
            // 
            // Timer_TicketLoadingGif
            // 
            this.Timer_TicketLoadingGif.Enabled = true;
            this.Timer_TicketLoadingGif.Interval = 50;
            this.Timer_TicketLoadingGif.Tick += new System.EventHandler(this.Timer_TicketLoadingGif_Tick);
            // 
            // Label_LoadingData
            // 
            this.Label_LoadingData.AutoSize = true;
            this.Label_LoadingData.Font = new System.Drawing.Font("Segoe UI Semibold", 12.10084F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_LoadingData.Location = new System.Drawing.Point(270, 386);
            this.Label_LoadingData.Name = "Label_LoadingData";
            this.Label_LoadingData.Size = new System.Drawing.Size(139, 30);
            this.Label_LoadingData.TabIndex = 1;
            this.Label_LoadingData.Text = "Loading data";
            // 
            // Timer_Label
            // 
            this.Timer_Label.Enabled = true;
            this.Timer_Label.Interval = 200;
            this.Timer_Label.Tick += new System.EventHandler(this.Timer_Label_Tick);
            // 
            // WindowLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(780, 430);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Label_LoadingData);
            this.Controls.Add(this.PictureBox_LoadingImage);
            this.Name = "WindowLoading";
            this.Text = "Loading Data";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_LoadingImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_LoadingImage;
        private System.Windows.Forms.Timer Timer_TicketLoadingGif;
        private System.Windows.Forms.Label Label_LoadingData;
        private System.Windows.Forms.Timer Timer_Label;
    }
}