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
            this.TextBox_Email = new System.Windows.Forms.TextBox();
            this.TextBox_Name = new System.Windows.Forms.TextBox();
            this.TextBox_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.TextBox_Password = new System.Windows.Forms.TextBox();
            this.Button_Create = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.TextBox_Surename = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextBox_Email
            // 
            this.TextBox_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Email.Location = new System.Drawing.Point(440, 250);
            this.TextBox_Email.Name = "TextBox_Email";
            this.TextBox_Email.Size = new System.Drawing.Size(434, 30);
            this.TextBox_Email.TabIndex = 1;
            // 
            // TextBox_Name
            // 
            this.TextBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Name.Location = new System.Drawing.Point(440, 130);
            this.TextBox_Name.Name = "TextBox_Name";
            this.TextBox_Name.Size = new System.Drawing.Size(434, 30);
            this.TextBox_Name.TabIndex = 2;
            // 
            // TextBox_ConfirmPassword
            // 
            this.TextBox_ConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_ConfirmPassword.Location = new System.Drawing.Point(440, 370);
            this.TextBox_ConfirmPassword.Name = "TextBox_ConfirmPassword";
            this.TextBox_ConfirmPassword.Size = new System.Drawing.Size(434, 30);
            this.TextBox_ConfirmPassword.TabIndex = 3;
            this.TextBox_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // TextBox_Password
            // 
            this.TextBox_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Password.Location = new System.Drawing.Point(440, 310);
            this.TextBox_Password.Name = "TextBox_Password";
            this.TextBox_Password.Size = new System.Drawing.Size(434, 30);
            this.TextBox_Password.TabIndex = 4;
            this.TextBox_Password.UseSystemPasswordChar = true;
            // 
            // Button_Create
            // 
            this.Button_Create.BackColor = System.Drawing.Color.Maroon;
            this.Button_Create.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Create.Location = new System.Drawing.Point(835, 484);
            this.Button_Create.Name = "Button_Create";
            this.Button_Create.Size = new System.Drawing.Size(200, 75);
            this.Button_Create.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Create.TabIndex = 10;
            this.Button_Create.Text = "Create";
            this.Button_Create.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Create.UseCustomBackColor = true;
            this.Button_Create.UseSelectable = true;
            this.Button_Create.UseStyleColors = true;
            this.Button_Create.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.Silver;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(277, 484);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(200, 75);
            this.Button_Cancel.Style = MetroFramework.MetroColorStyle.Black;
            this.Button_Cancel.TabIndex = 13;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.UseStyleColors = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // TextBox_Surename
            // 
            this.TextBox_Surename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Surename.Location = new System.Drawing.Point(440, 190);
            this.TextBox_Surename.Name = "TextBox_Surename";
            this.TextBox_Surename.Size = new System.Drawing.Size(434, 30);
            this.TextBox_Surename.TabIndex = 14;
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.TextBox_Surename);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Create);
            this.Controls.Add(this.TextBox_Password);
            this.Controls.Add(this.TextBox_ConfirmPassword);
            this.Controls.Add(this.TextBox_Name);
            this.Controls.Add(this.TextBox_Email);
            this.Name = "CreateUser";
            this.Text = "Create User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Email;
        private System.Windows.Forms.TextBox TextBox_Name;
        private System.Windows.Forms.TextBox TextBox_ConfirmPassword;
        private System.Windows.Forms.TextBox TextBox_Password;
        private MetroFramework.Controls.MetroButton Button_Create;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private System.Windows.Forms.TextBox TextBox_Surename;
    }
}