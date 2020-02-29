using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TC37852369.Helpers;
using TC37852369.Services.Images;

namespace TC37852369.UI
{
    public partial class WindowLoading : MetroForm
    {
        MainWindow mainWindow;
        string workingDirectory = Environment.CurrentDirectory;
        string imagePath;
        GifImage gifImage;
        public WindowLoading(MainWindow mainWindow)
        {
            InitializeComponent();
            this.FormClosed += CloseHandler;
           
            this.mainWindow = mainWindow;
            this.mainWindow.Enabled = false;

            imagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\ticket_loading.gif";
            gifImage = new GifImage(imagePath, 400, 300);
            gifImage.ReverseAtEnd = false;
            PictureBox_LoadingImage.Image = gifImage.GetNextFrame();
            PictureBox_LoadingImage.BringToFront();

            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            BringToFront();
        }
        private void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void Timer_TicketLoadingGif_Tick(object sender, EventArgs e)
        {
            PictureBox_LoadingImage.Image = gifImage.GetNextFrame();
            
        }

        private void Timer_Label_Tick(object sender, EventArgs e)
        {
            if (Label_LoadingData.Text.Equals("Loading data"))
            {
                Label_LoadingData.Text = "Loading data .";
            }
            else if (Label_LoadingData.Text.Equals("Loading data ."))
            {
                Label_LoadingData.Text = "Loading data . .";
            }
            else if (Label_LoadingData.Text.Equals("Loading data . ."))
            {
                Label_LoadingData.Text = "Loading data . . .";
            }
            else if (Label_LoadingData.Text.Equals("Loading data . . ."))
            {
                Label_LoadingData.Text = "Loading data";
            }
        }
    }
}
