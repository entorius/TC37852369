namespace TC37852369
{
    partial class Login
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
            this.TextBox_Email = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Button_Login = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // TextBox_Email
            // 
            this.TextBox_Email.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Email.Location = new System.Drawing.Point(95, 114);
            this.TextBox_Email.Name = "TextBox_Email";
            this.TextBox_Email.Size = new System.Drawing.Size(362, 35);
            this.TextBox_Email.TabIndex = 1;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Password.Location = new System.Drawing.Point(95, 208);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(362, 35);
            this.TextBox_Password.TabIndex = 2;
            this.TextBox_Password.UseSystemPasswordChar = true;
            // 
            // Button_Login
            // 
            this.Button_Login.BackColor = System.Drawing.Color.DodgerBlue;
            this.Button_Login.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.Button_Login.Location = new System.Drawing.Point(202, 315);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(136, 51);
            this.Button_Login.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Login.TabIndex = 11;
            this.Button_Login.Text = "Login";
            this.Button_Login.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Login.UseCustomBackColor = true;
            this.Button_Login.UseSelectable = true;
            this.Button_Login.UseStyleColors = true;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 500);
            this.Controls.Add(this.Button_Login);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.TextBox_Email);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Email;
        private System.Windows.Forms.TextBox TextBox_Password;
        private MetroFramework.Controls.MetroButton Button_Login;
    }
}