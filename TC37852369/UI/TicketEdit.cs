using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum Colors
{
    Black,
    Blue,
    Green,
    Red,
    White,
    Orange
}
public enum ColorsHexes
{
    O,
    Blue,
    Green,
    Red,
    FFFFFF,
    Orange
}

namespace TC37852369.UI
{
    public partial class TicketEdit : MetroForm
    {
        public TicketEdit()
        {
            InitializeComponent();
            TextBox_HEXLineColor.TextChanged += HexLineColorChanged;
            BringToFront();
        }

        private void HexLineColorChanged(object sender, EventArgs e)
        {
            PictureBox_LineColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + TextBox_HEXLineColor.Text);
        }

        private void TextBox_HEXLineColor_Click(object sender, EventArgs e)
        {

        }
    }
}
