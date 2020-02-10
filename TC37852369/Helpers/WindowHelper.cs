using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369.Helpers
{
    public static class WindowHelper
    {
        public static void GoFullscreen(bool fullscreen, MetroForm form)
        {
            if (fullscreen)
            {
                form.WindowState = FormWindowState.Normal;
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                form.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                form.WindowState = FormWindowState.Maximized;
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
        public static bool checkIfMaximizeWindow(int width, int height)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            if (width  > (screenWidth - 30) || height > (screenHeight - 30))
            {
                return true;
            }
            return false;
        }
    }
}
