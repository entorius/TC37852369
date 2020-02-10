namespace TC37852369.UI
{
    partial class GenerateSendInfoWindow
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
            this.PictureBox_Status = new System.Windows.Forms.PictureBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_Confirm = new System.Windows.Forms.Button();
            this.Timer_Document = new System.Windows.Forms.Timer(this.components);
            this.Timer_Sending = new System.Windows.Forms.Timer(this.components);
            this.Timer_Sent = new System.Windows.Forms.Timer(this.components);
            this.Label_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Status)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_Status
            // 
            this.PictureBox_Status.Location = new System.Drawing.Point(249, 50);
            this.PictureBox_Status.Name = "PictureBox_Status";
            this.PictureBox_Status.Size = new System.Drawing.Size(300, 300);
            this.PictureBox_Status.TabIndex = 0;
            this.PictureBox_Status.TabStop = false;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.White;
            this.Button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Cancel.Font = new System.Drawing.Font("Segoe UI Semibold", 7.865546F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Button_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button_Cancel.Location = new System.Drawing.Point(108, 356);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(150, 58);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = false;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Button_Confirm.Enabled = false;
            this.Button_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Confirm.Font = new System.Drawing.Font("Segoe UI Semibold", 7.865546F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Button_Confirm.ForeColor = System.Drawing.Color.White;
            this.Button_Confirm.Location = new System.Drawing.Point(515, 356);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(150, 58);
            this.Button_Confirm.TabIndex = 2;
            this.Button_Confirm.Text = "Confirm";
            this.Button_Confirm.UseVisualStyleBackColor = false;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // Timer_Document
            // 
            this.Timer_Document.Enabled = true;
            this.Timer_Document.Tick += new System.EventHandler(this.Timer_Document_Tick);
            // 
            // Timer_Sending
            // 
            this.Timer_Sending.Tick += new System.EventHandler(this.Timer_Sending_Tick);
            // 
            // Timer_Sent
            // 
            this.Timer_Sent.Tick += new System.EventHandler(this.Timer_Sent_Tick);
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.Font = new System.Drawing.Font("Segoe UI Semibold", 12.10084F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Label_Status.ForeColor = System.Drawing.Color.DarkViolet;
            this.Label_Status.Location = new System.Drawing.Point(260, 279);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(0, 28);
            this.Label_Status.TabIndex = 3;
            this.Label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GenerateSendInfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(800, 450);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Label_Status);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.PictureBox_Status);
            this.Name = "GenerateSendInfoWindow";
            this.Text = "Sending";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_Status;
        public System.Windows.Forms.Button Button_Cancel;
        public System.Windows.Forms.Button Button_Confirm;
        public System.Windows.Forms.Timer Timer_Document;
        public System.Windows.Forms.Timer Timer_Sending;
        public System.Windows.Forms.Timer Timer_Sent;
        public System.Windows.Forms.Label Label_Status;
    }
}