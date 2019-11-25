namespace TC37852369
{
    partial class EmailTemplate
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
            this.TextBox_Subject = new System.Windows.Forms.TextBox();
            this.TextBox_Body = new System.Windows.Forms.TextBox();
            this.Button_Cancel = new TC37852369.CircularButton();
            this.Button_Confirm = new TC37852369.CircularButton();
            this.SuspendLayout();
            // 
            // TextBox_Subject
            // 
            this.TextBox_Subject.Location = new System.Drawing.Point(63, 56);
            this.TextBox_Subject.Multiline = true;
            this.TextBox_Subject.Name = "TextBox_Subject";
            this.TextBox_Subject.Size = new System.Drawing.Size(463, 53);
            this.TextBox_Subject.TabIndex = 1;
            // 
            // TextBox_Body
            // 
            this.TextBox_Body.Location = new System.Drawing.Point(63, 128);
            this.TextBox_Body.Multiline = true;
            this.TextBox_Body.Name = "TextBox_Body";
            this.TextBox_Body.Size = new System.Drawing.Size(463, 248);
            this.TextBox_Body.TabIndex = 2;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(63, 402);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(105, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.Location = new System.Drawing.Point(429, 402);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(97, 23);
            this.Button_Confirm.TabIndex = 4;
            this.Button_Confirm.Text = "Confirm";
            this.Button_Confirm.UseVisualStyleBackColor = true;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // EmailTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 450);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.TextBox_Body);
            this.Controls.Add(this.TextBox_Subject);
            this.Name = "EmailTemplate";
            this.Text = "Email Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Subject;
        private System.Windows.Forms.TextBox TextBox_Body;
        private CircularButton Button_Cancel;
        private CircularButton Button_Confirm;
    }
}