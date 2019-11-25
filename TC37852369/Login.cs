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
    public partial class Login : MetroForm
    {
        public Login()
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

        private void Button_Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            GenerateSend sending = new GenerateSend();
            sending.Show();
        }
    }
}
