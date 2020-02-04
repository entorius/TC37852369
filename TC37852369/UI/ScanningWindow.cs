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
using TC37852369.DomainEntities;
using TC37852369.Services.Encoder;
using TC37852369.Services.Images;
using TC37852369.UI.helpers;

namespace TC37852369.UI
{
    public partial class ScanningWindow : MetroForm
    {
        string workingDirectory = Environment.CurrentDirectory;
        string imagePath;
        GifImage gifImage;
        MainWindow mainWindow;
        string lastBarcodeTextBoxValue = "";
        List<Event> events = new List<Event>();
        List<Participant> participants = new List<Participant>();
        MetroMessageBoxHelper metroMessageBoxHelper = new MetroMessageBoxHelper();
        

        public ScanningWindow(MainWindow mainWindow, List<Event> events, List<Participant> participants)
        {
            InitializeComponent();
            this.FormClosed += CloseHandler;

            this.mainWindow = mainWindow;
            this.participants = participants;
            this.events = events;

            imagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\qr-scan.gif";
            gifImage = new GifImage(imagePath,264,132);
            gifImage.ReverseAtEnd = false;
            PictureBox_Barcode.Image = gifImage.GetNextFrame();
            PictureBox_Barcode.BringToFront();

            this.ActiveControl = TextBox_Barcode;
        }
        private void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }
        private void Timer_Gif_Tick(object sender, EventArgs e)
        {
            PictureBox_Barcode.Image = gifImage.GetNextFrame();
            
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainWindow.Enabled = true;
        }

        private void Timer_Barcode_Tick(object sender, EventArgs e)
        {
            if(TextBox_Barcode.Text.Length > 0)
            {
                if(lastBarcodeTextBoxValue.Length == TextBox_Barcode.Text.Length)
                {
                    string decodedBarcode = StringEncoder.ReturnDecryptedString(TextBox_Barcode.Text);
                    if (decodedBarcode.Length > 0)
                    {
                        this.Hide();
                        ScanningUserDisplay scanningUserDisplay =
                            new ScanningUserDisplay(this, mainWindow, participants, events, decodedBarcode);
                        scanningUserDisplay.Show();
                        scanningUserDisplay.BringToFront();
                        Timer_Barcode.Enabled = false;
                    }
                    else
                    {
                        TextBox_Barcode.Text = "";
                        Label_Scanning.Text = "";
                        metroMessageBoxHelper.showWarning(this,
                            "1) Scanning window was not focused while scanning\n" +
                            "2) Barcode was in incorrect format\n" +
                            "3) If you haven't choose English language on your computer", "Warning");
                    }
                }
                else
                {
                    lastBarcodeTextBoxValue = TextBox_Barcode.Text;
                    if (Label_Scanning.Text.Equals("") || Label_Scanning.Text.Equals("Scanning..."))
                        Label_Scanning.Text = "Scanning.";
                    else if (Label_Scanning.Text.Equals("Scanning."))
                        Label_Scanning.Text = "Scanning..";
                    else  if (Label_Scanning.Text.Equals("Scanning.."))
                        Label_Scanning.Text = "Scanning...";
                }
            }
        }
    }
}
