using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369
{
    public class TextBoxModification
    {
        string defaultText;
        bool isPassword;
        TextBox textBox;
        public TextBoxModification(TextBox textBox,string defaultText,bool isPassword)
        {
            if (isPassword)
            {
                textBox.UseSystemPasswordChar = false;
            }
            this.defaultText = defaultText;
            this.textBox = textBox;
            textBox.Text = defaultText;
            textBox.ForeColor = SystemColors.GrayText;
            this.isPassword = isPassword;
            
        }
        public void addEvents()
        {
            if (isPassword)
            {
                textBox.Leave += TextBox_LeavePass;
                textBox.Enter += TextBox_EnterPass;
            }
            else
            {
                textBox.Leave += TextBox_Leave;
                textBox.Enter += TextBox_Enter;
            }
            
        }
        private void TextBox_LeavePass(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                textBox.UseSystemPasswordChar = false;
                textBox.Text = defaultText;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_EnterPass(object sender, EventArgs e)
        {
            if (textBox.Text == defaultText)
            {
                textBox.UseSystemPasswordChar = true;
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                textBox.Text = defaultText;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBox.Text == defaultText)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }
    }
}
