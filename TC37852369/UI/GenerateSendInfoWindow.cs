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
using TC37852369.Services.Ticket_generation;

namespace TC37852369.UI
{
    public partial class GenerateSendInfoWindow : MetroForm
    {
        string workingDirectory = Environment.CurrentDirectory;
       
        string generatingDocumentGifPath;
        string sendingGifPath;
        string sentGifPath;

        GifImage generatingDocumentGif;
        GifImage sendingGif;
        GifImage sentGif;
        GenerateSend generateSend;
        EditParticipant editParticipant;


        public GenerateSendInfoWindow(GenerateSend generateSend)
        {
            initializeWindow();

            this.generateSend = generateSend;
            InitializeComponent();
        }
        public GenerateSendInfoWindow(EditParticipant editParticipant)
        {
            initializeWindow();

            this.editParticipant = editParticipant;
            InitializeComponent();
        }

        private void initializeWindow()
        {
            sendingGifPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\Sending.gif";
            generatingDocumentGifPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\GeneratingDocument.gif";
            sentGifPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\Sent.gif";

            generatingDocumentGif = new GifImage(generatingDocumentGifPath, 300, 225);
            sendingGif = new GifImage(sendingGifPath, 300, 300);
            sentGif = new GifImage(sentGifPath, 300, 225);
        }
        private void Timer_Document_Tick(object sender, EventArgs e)
        {
            PictureBox_Status.Image = generatingDocumentGif.GetNextFrame();
        }

        private void Timer_Sending_Tick(object sender, EventArgs e)
        {
            PictureBox_Status.Image = sendingGif.GetNextFrame();
        }

        private void Timer_Sent_Tick(object sender, EventArgs e)
        {
            PictureBox_Status.Image = sentGif.GetNextFrame();
            if (!Button_Confirm.Enabled)
            {
                Button_Confirm.Enabled = true;
                Button_Cancel.Enabled = false;
            }
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            FileHelper fileHelper = new FileHelper();
            string workingDirectory = TicketCreation.workingDirectory;
            string directory = Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\docs";
            fileHelper.DeleteAllFilesFormDirectory(directory);
            if (generateSend != null)
            {
                generateSend.Button_Send.Enabled = true;
            }
            if (editParticipant != null)
            {
                editParticipant.Button_Send.Enabled = true;
            }
            this.Dispose();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            if (generateSend != null)
            {
                generateSend.cancelationTokenSource.Cancel();
                generateSend.Button_Send.Enabled = true;
            }
            if (editParticipant != null)
            {
                editParticipant.cancelationTokenSource.Cancel();
                editParticipant.Button_Send.Enabled = true;
            }
                this.Dispose();
        }
    }
}
