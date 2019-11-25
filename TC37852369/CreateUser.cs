using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TC37852369
{
    public partial class CreateUser : MetroForm
    {
        public CreateUser()
        {
            InitializeComponent();
            TextBox_Email.ForeColor = SystemColors.GrayText;
            TextBox_Email.Text = "Email";
            this.TextBox_Email.Leave += new System.EventHandler(this.TextBox_Email_Leave);
            this.TextBox_Email.Enter += new System.EventHandler(this.TextBox_Email_Enter);

            TextBox_Password.UseSystemPasswordChar = false;
            TextBox_Password.ForeColor = SystemColors.GrayText;
            TextBox_Password.Text = "Password";
            this.TextBox_Password.Leave += new System.EventHandler(this.TextBox_Password_Leave);
            this.TextBox_Password.Enter += new System.EventHandler(this.TextBox_Password_Enter);

            TextBox_Password.UseSystemPasswordChar = false;
            TextBox_ConfirmPassword.ForeColor = SystemColors.GrayText;
            TextBox_ConfirmPassword.Text = "Confirm password";
            this.TextBox_ConfirmPassword.Leave += new System.EventHandler(this.TextBox_ConfirmPassword_Leave);
            this.TextBox_ConfirmPassword.Enter += new System.EventHandler(this.TextBox_ConfirmPassword_Enter);

            TextBox_Name.ForeColor = SystemColors.GrayText;
            TextBox_Name.Text = "Name";
            this.TextBox_Name.Leave += new System.EventHandler(this.TextBox_Name_Leave);
            this.TextBox_Name.Enter += new System.EventHandler(this.TextBox_Name_Enter);

        }

        private void Button_CreateUser_Click(object sender, EventArgs e)
        {

        }
        private void TextBox_Email_Leave(object sender, EventArgs e)
        {
            if (TextBox_Email.Text.Length == 0)
            {
                TextBox_Email.Text = "Email";
                TextBox_Email.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Email_Enter(object sender, EventArgs e)
        {
            if (TextBox_Email.Text == "Email")
            {
                TextBox_Email.Text = "";
                TextBox_Email.ForeColor = SystemColors.WindowText;
            }
        }
        private void TextBox_Password_Leave(object sender, EventArgs e)
        {
            if (TextBox_Password.Text.Length == 0)
            {
                TextBox_Password.UseSystemPasswordChar = false;
                TextBox_Password.Text = "Password";
                TextBox_Password.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Password_Enter(object sender, EventArgs e)
        {
            if (TextBox_Password.Text == "Password")
            {
                TextBox_Password.UseSystemPasswordChar = true;
                TextBox_Password.Text = "";
                TextBox_Password.ForeColor = SystemColors.WindowText;
            }
        }
        private void TextBox_ConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (TextBox_ConfirmPassword.Text.Length == 0)
            {
                TextBox_ConfirmPassword.UseSystemPasswordChar = false;
                TextBox_ConfirmPassword.Text = "Confirm password";
                TextBox_ConfirmPassword.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_ConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (TextBox_ConfirmPassword.Text == "Confirm password")
            {
                TextBox_ConfirmPassword.UseSystemPasswordChar = true;
                TextBox_ConfirmPassword.Text = "";
                TextBox_ConfirmPassword.ForeColor = SystemColors.WindowText;
            }
        }
        private void TextBox_Name_Leave(object sender, EventArgs e)
        {
            if (TextBox_Name.Text.Length == 0)
            {
                TextBox_Name.Text = "Name";
                TextBox_Name.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Name_Enter(object sender, EventArgs e)
        {
            if (TextBox_Name.Text == "Name")
            {
                TextBox_Name.Text = "";
                TextBox_Name.ForeColor = SystemColors.WindowText;
            }
        }

    }
}
