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
    public partial class GenerateTicket : MetroForm
    {
        public GenerateTicket()
        {
            InitializeComponent();
            TextBox_UserId.ForeColor = SystemColors.GrayText;
            TextBox_UserId.Text = "User Id";
            this.TextBox_UserId.Leave += new System.EventHandler(this.TextBox_UserId_Leave);
            this.TextBox_UserId.Enter += new System.EventHandler(this.TextBox_UserId_Enter);

        }

        private void Button_Generate_Click(object sender, EventArgs e)
        {

        }

        private void TextBox_UserId_Leave(object sender, EventArgs e)
        {
            if (TextBox_UserId.Text.Length == 0)
            {
                TextBox_UserId.UseSystemPasswordChar = false;
                TextBox_UserId.Text = "User Id";
                TextBox_UserId.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_UserId_Enter(object sender, EventArgs e)
        {
            if (TextBox_UserId.Text == "User Id")
            {
                TextBox_UserId.UseSystemPasswordChar = true;
                TextBox_UserId.Text = "";
                TextBox_UserId.ForeColor = SystemColors.WindowText;
            }
        }
    }
}
