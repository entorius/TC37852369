using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.Services.Ticket_generation;
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class GenerateTicket : MetroForm
    {
        MainWindow mainWindow;
        TicketCreation ticketCreation = new TicketCreation();
        BarcodeGenerator barcodeGenerator = new BarcodeGenerator();
        MetroMessageBoxHelper metroMessageBoxHelper = new MetroMessageBoxHelper();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        ParticipantServices participantServices = new ParticipantServices();
        CompanyData companyData;
        List<Event> eventsEntities;
        Dictionary<int, string> eventImagesPaths = new Dictionary<int, string>();
        ImageEntityServices imageEntityServices = new ImageEntityServices();
        private static string workingDirectory = Environment.CurrentDirectory;
        string targetImagesDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\tempImages\";
        public GenerateTicket(MainWindow window, CompanyData companyData, List<Event> eventsEntities)
        {
           
            this.companyData = companyData;
            this.eventsEntities = eventsEntities;
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            foreach(Event ev in eventsEntities)
            {
                ComboBox_Events.Items.Add(ev.eventName);
            }
        }
        private async void Button_Generate_Click(object sender, EventArgs e)
        {
            Button_Generate.Enabled = false;
            Label_Generating.Text = "Generating";
            if (ComboBox_Events.SelectedIndex >= 0)
            {
                if (DialogResult.OK == FolderBrowserDialog_Generation.ShowDialog())
                {
                    string savingPath = FolderBrowserDialog_Generation.SelectedPath;

                    List<ImageEntity> imageEntities= await imageEntityServices.GetEventImageEntities(eventsEntities[ComboBox_Events.SelectedIndex]);
                    foreach (ImageEntity en in imageEntities)
                    {
                        string imagePath = await imageEntityServices.downloadEventImage(en, targetImagesDirectory);
                        eventImagesPaths.Add(Int32.Parse(en.imageNumber.ToString()), imagePath);
                    }
                    List<ImageEntity> companyImage = await imageEntityServices.GetCompanyImageEntities();

                    

                    string companyImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\inlinum-logo.png";
                    if (companyImage.Count > 0)
                    {
                        companyImagePath = await imageEntityServices.downloadCompanyImage(companyImage[0], targetImagesDirectory);
                    }

                    string eventImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\logo_final.png";
                    if (eventImagesPaths.ContainsKey(1))
                    {
                        eventImagePath = eventImagesPaths[1];
                    }
                    string sponsorsImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\sponsorImage1.png";
                    if (eventImagesPaths.ContainsKey(2))
                    {
                        sponsorsImagePath = eventImagesPaths[2];
                    }
                    if (ticketCreation.MSdoc == null) { ticketCreation.MSdoc = new Microsoft.Office.Interop.Word.Application(); }
                    ticketCreation.pdfConverter = new PDFConverter(ticketCreation.MSdoc);
                    string ticketPath = ticketCreation.createTicket("Serafina", "Jones", "Daimter", "Speaker",
                        "Green Auto Summit 2020", "Tuesday , 31 March 2020", "Golden Gate Park", "San Francisco, CA", "IL682370000000",
                        Color.Black, "testTicket", companyImagePath, eventImagePath, sponsorsImagePath,savingPath);
                }
                if (ticketCreation.MSdoc != null)
                {
                    object Unknown = Type.Missing;
                    ticketCreation.MSdoc.Documents.Close(ref Unknown, ref Unknown, ref Unknown);
                    ticketCreation.MSdoc.Quit(ref Unknown, ref Unknown, ref Unknown);
                }
                Label_Generating.Text = "";
                Button_Generate.Enabled = true;
            }
            else
            {
                metroMessageBoxHelper.showWarning(this, "You have not choosen event!", "Warning");
            }

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }

        protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void Button_GenerateBarcode_Click(object sender, EventArgs e)
        {
            /*barcodeGenerator.generateQRBarcode("Something", Color.Black);*/
        }

        private /*async*/ void Button_GenerateBarcodeNumber_Click(object sender, EventArgs e)
        {
            /*string barcode = await barcodeGenerator.generateBarcodeNumber("1");
            TextBox_Barcode.Text = barcode; */

        }
        public string formatEventDate(DateTime date)
        {

            CultureInfo usEnglish = new CultureInfo("en-US");
            string month = usEnglish.DateTimeFormat.GetMonthName(date.Month);
            string dayOfWeek =  date.DayOfWeek.ToString();
            string day = date.Day.ToString();
            string year = date.Year.ToString();

            return dayOfWeek + ", " + day + " " + month + " " + year;


        }

        private /*async*/ void Button_GenerateUsers_Click(object sender, EventArgs e)
        {
            /*List<Participant> addedPartcipants = new List<Participant>();
            for (int i = 0; i < 100; i++)
            {
               Participant addedParticipant = await participantServices.createParticipant(
                   eventEntity.id.ToString(),
                   "Alexander" + i.ToString(),
                   "Zharkov" + i.ToString(),
                   "Procastinator",
                   "ITForVilnius",
                   "Service",
                   "alexanderzharkov06@gmail.com",
                   "862215666646",
                   "Lithuania",
                   "Delegate",
                   "Due",
                   true,
                   true,
                   true,
                   false,
                   false,
                   false
                   );
                addedPartcipants.Add(addedParticipant);
            }
            mainWindow.Enabled = true;
            this.Dispose();*/
        }
    }
}
