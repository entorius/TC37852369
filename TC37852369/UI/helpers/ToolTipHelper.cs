using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369.UI.helpers
{
    public class ToolTipHelper
    {
        public ToolTip labelToolTip = new ToolTip();
        public ToolTip getLabelToolTip(Label label,string text )
        {
            //The below are optional, of course,

            labelToolTip.ToolTipIcon = ToolTipIcon.Info;
            labelToolTip.OwnerDraw = true;
            labelToolTip.IsBalloon = true;
            labelToolTip.ShowAlways = true;
            labelToolTip.SetToolTip(label, text);
            labelToolTip.Draw += new DrawToolTipEventHandler(toolTip1_Draw);
            return labelToolTip;
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font f = new Font("Segoe UI Semibold", 10.0f);
            labelToolTip.BackColor = Color.Black;
            labelToolTip.ForeColor = Color.White;
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2, 2));
        }
    }
}
