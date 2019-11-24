namespace TC37852369
{
    partial class CreateUser
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
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_Email = new System.Windows.Forms.TextBox();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.TextBox_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Button_Cancel = new TC37852369.CircularButton();
            this.Button_CreateUser = new TC37852369.CircularButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create User";
            // 
            // TextBox_Email
            // 
            this.TextBox_Email.Location = new System.Drawing.Point(135, 105);
            this.TextBox_Email.Name = "TextBox_Email";
            this.TextBox_Email.Size = new System.Drawing.Size(434, 22);
            this.TextBox_Email.TabIndex = 1;
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Location = new System.Drawing.Point(135, 243);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(434, 22);
            this.TextBox_Name.TabIndex = 2;
            // 
            // TextBox_ConfirmPassword
            // 
            this.TextBox_ConfirmPassword.Location = new System.Drawing.Point(135, 201);
            this.TextBox_ConfirmPassword.Name = "TextBox_ConfirmPassword";
            this.TextBox_ConfirmPassword.Size = new System.Drawing.Size(434, 22);
            this.TextBox_ConfirmPassword.TabIndex = 3;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Location = new System.Drawing.Point(135, 156);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(434, 22);
            this.TextBox_Password.TabIndex = 4;
            this.TextBox_Password.UseSystemPasswordChar = true;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(135, 340);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 5;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Button_CreateUser
            // 
            this.Button_CreateUser.Location = new System.Drawing.Point(494, 340);
            this.Button_CreateUser.Name = "Button_CreateUser";
            this.Button_CreateUser.Size = new System.Drawing.Size(75, 23);
            this.Button_CreateUser.TabIndex = 6;
            this.Button_CreateUser.Text = "Create";
            this.Button_CreateUser.UseVisualStyleBackColor = true;
            this.Button_CreateUser.Click += new System.EventHandler(this.Button_CreateUser_Click);
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 450);
            this.Controls.Add(this.Button_CreateUser);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.TextBox_ConfirmPassword);
            this.Controls.Add(this.TextBox_Name);
            this.Controls.Add(this.TextBox_Email);
            this.Controls.Add(this.label1);
            this.Name = "CreateUser";
            this.Text = "Create_User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_Email;
        private System.Windows.Forms.TextBox TextBox_Name;
        private System.Windows.Forms.TextBox TextBox_ConfirmPassword;
        private System.Windows.Forms.TextBox TextBox_Password;
        private CircularButton Button_Cancel;
        private CircularButton Button_CreateUser;
    }
}