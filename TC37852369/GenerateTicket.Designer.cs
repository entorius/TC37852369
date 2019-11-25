namespace TC37852369
{
    partial class GenerateTicket
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
            this.TextBox_UserId = new System.Windows.Forms.TextBox();
            this.Button_Generate = new TC37852369.CircularButton();
            this.Button_Cancel = new TC37852369.CircularButton();
            this.SuspendLayout();
            // 
            // TextBox_UserId
            // 
            this.TextBox_UserId.Location = new System.Drawing.Point(151, 78);
            this.TextBox_UserId.Name = "TextBox_UserId";
            this.TextBox_UserId.Size = new System.Drawing.Size(281, 22);
            this.TextBox_UserId.TabIndex = 1;
            // 
            // Button_Generate
            // 
            this.Button_Generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Button_Generate.Location = new System.Drawing.Point(374, 141);
            this.Button_Generate.Name = "Button_Generate";
            this.Button_Generate.Size = new System.Drawing.Size(102, 40);
            this.Button_Generate.TabIndex = 2;
            this.Button_Generate.Text = "Generate";
            this.Button_Generate.UseVisualStyleBackColor = false;
            this.Button_Generate.Click += new System.EventHandler(this.Button_Generate_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(126, 150);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // GenerateTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 252);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Generate);
            this.Controls.Add(this.TextBox_UserId);
            this.Name = "GenerateTicket";
            this.Text = "GenerateTicket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_UserId;
        private CircularButton Button_Generate;
        private CircularButton Button_Cancel;
    }
}