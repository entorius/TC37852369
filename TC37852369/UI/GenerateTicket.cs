using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Services;
using TC37852369.Services.Ticket_generation;

namespace TC37852369
{
    public partial class GenerateTicket : MetroForm
    {
        MainWindow mainWindow;
        TicketCreation ticketCreation = new TicketCreation();
        BarcodeGenerator barcodeGenerator = new BarcodeGenerator();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        ParticipantServices participantServices = new ParticipantServices();
        CompanyData companyData;
        Participant participant;
        Event eventEntity;
        public GenerateTicket(MainWindow window, Participant participant, CompanyData companyData, Event eventEntity)
        {
            this.participant = participant;
            this.companyData = companyData;
            this.eventEntity = eventEntity;
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            TextBoxModification TextBox_UserIdMod = new TextBoxModification(TextBox_UserId, "User Id", false);
            TextBox_UserIdMod.addEvents();
        }
        private void Button_Generate_Click(object sender, EventArgs e)
        {
            Image image = ticketCreation.GenerateCompanyCredentialsImage(companyData.companyName,
                companyData.address, companyData.email, companyData.phoneNumber, eventEntity.webPage);

            List<Participant> participants = new List<Participant>();
            participants.Add(participant);
            List<Event> events = new List<Event>();
            events.Add(eventEntity);

            ticketCreation.generateTicketsAndSave(participants, events, companyData);

            mainWindow.Enabled = true;
            this.Dispose();
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
            barcodeGenerator.generateQRBarcode("Something", Color.Black);
        }

        private async void Button_GenerateBarcodeNumber_Click(object sender, EventArgs e)
        {
            string barcode = await barcodeGenerator.generateBarcodeNumber("1");
            TextBox_Barcode.Text = barcode; 

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

        private async void Button_GenerateUsers_Click(object sender, EventArgs e)
        {
            List<Participant> addedPartcipants = new List<Participant>();
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
            this.Dispose();
        }
    }
}
