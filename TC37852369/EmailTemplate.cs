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
    public partial class EmailTemplate : MetroForm
    {
        public EmailTemplate()
        {
            InitializeComponent();
            TextBox_Subject.ForeColor = SystemColors.GrayText;
            TextBox_Subject.Text = "Subject";
            this.TextBox_Subject.Leave += new System.EventHandler(this.TextBox_Subject_Leave);
            this.TextBox_Subject.Enter += new System.EventHandler(this.TextBox_Subject_Enter);

            TextBox_Body.ForeColor = SystemColors.GrayText;
            TextBox_Body.Text = "Body";
            this.TextBox_Body.Leave += new System.EventHandler(this.TextBox_Body_Leave);
            this.TextBox_Body.Enter += new System.EventHandler(this.TextBox_Body_Enter);
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {

        }
        private void TextBox_Subject_Leave(object sender, EventArgs e)
        {
            if (TextBox_Subject.Text.Length == 0)
            {
                TextBox_Subject.UseSystemPasswordChar = false;
                TextBox_Subject.Text = "Subject";
                TextBox_Subject.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Subject_Enter(object sender, EventArgs e)
        {
            if (TextBox_Subject.Text == "Subject")
            {
                TextBox_Subject.UseSystemPasswordChar = true;
                TextBox_Subject.Text = "";
                TextBox_Subject.ForeColor = SystemColors.WindowText;
            }
        }

        private void TextBox_Body_Leave(object sender, EventArgs e)
        {
            if (TextBox_Body.Text.Length == 0)
            {
                TextBox_Body.UseSystemPasswordChar = false;
                TextBox_Body.Text = "Body";
                TextBox_Body.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Body_Enter(object sender, EventArgs e)
        {
            if (TextBox_Body.Text == "Body")
            {
                TextBox_Body.UseSystemPasswordChar = true;
                TextBox_Body.Text = "";
                TextBox_Body.ForeColor = SystemColors.WindowText;
            }
        }
    }
}
