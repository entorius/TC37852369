using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI;

namespace TC37852369
{
    public partial class MainWindow : MetroForm
    {
        Login login;
        //events variables
        public EventServices eventService = new EventServices();
        public List<Event> events = new List<Event>();
        public ParticipantServices participantService = new ParticipantServices();
        public List<Participant> participants = new List<Participant>();
        public MainWindow(Login login)
        {
            this.login = login;
            this.FormClosed += ClosedHandler;
            InitializeComponent();
            loadData();
        }
        public async void loadData()
        {
            events = await eventService.getAllEvents();
            participants = await participantService.getAllParticipants();

            UpdateEventsTable();
            UpdateParticipantsTable();
        }
        //Main Window Events tab actions
        public void UpdateEventsTable()
        {
            
            
            Table_EventsData.RowCount = events.Count;
            for(int i = 0; i < events.Count - 1; i++)
            {
                Table_EventsData.RowStyles.Add(new RowStyle(SizeType.Absolute));
            }
            TableLayoutRowStyleCollection styles =
            Table_EventsData.RowStyles;
            foreach (RowStyle style in styles)
            {
                // Set the row height to 20 pixels.
                style.SizeType = SizeType.Absolute;
                style.Height = 40;
            }
            for (int i = 0; i < events.Count; i++)
            {
                addEventToEventTableRow(events[i], i);
            }
            
        }
        private void Button_CreateEvent_Click(object sender, EventArgs e)
        {
            CreateEvent cEvent = new CreateEvent(this);
            this.Enabled = false;
            cEvent.Show();
        }
        public void addEventTableRow()
        {
            this.addTableRow(Table_EventsData);
        }
        public void addEventToEventTableRow(Event eventEntity, int rowNumber)
        {
            Label label_EventName = new Label();
            label_EventName.Text = eventEntity.eventName;
            label_EventName.Width = 300;
            Label label_EventDate = new Label();
            label_EventDate.Text = eventEntity.date_From.ToString("yyyy/MM/dd");
            label_EventDate.Width = 200;
            Label label_EventDuration = new Label();
            label_EventDuration.Text = eventEntity.eventLengthDays.ToString();
            label_EventDuration.Width = 150;
            Label label_VenueName = new Label();
            label_VenueName.Text = eventEntity.venueName;
            label_VenueName.Width = 200;
            Label label_VenueAdress = new Label();
            label_VenueAdress.Text = eventEntity.venueAdress;
            label_VenueAdress.Width = 200;
            Label label_Status = new Label();
            label_Status.Text = eventEntity.eventStatus;
            label_Status.Width = 160;
            Label label_SmailTemplate = new Label();
            label_SmailTemplate.Text = eventEntity.current_Mail_Template;
            label_SmailTemplate.Width = 320;

            MetroButton button_Edit = new MetroButton();
            button_Edit.Height = 40;
            button_Edit.Width = 80;
            button_Edit.Text = "Edit";
            button_Edit.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            button_Edit.BackColor = ColorTranslator.FromHtml("#626262");
            button_Edit.ForeColor = Color.White;
            button_Edit.UseCustomBackColor = true;
            button_Edit.UseCustomForeColor = true;
            button_Edit.MouseClick += ButtonEditEventHandler;


            Table_EventsData.Controls.Add(label_EventName, 0, rowNumber);
            Table_EventsData.Controls.Add(label_EventDate, 1, rowNumber);
            Table_EventsData.Controls.Add(label_EventDuration, 2, rowNumber);
            Table_EventsData.Controls.Add(label_VenueName, 3, rowNumber);
            Table_EventsData.Controls.Add(label_VenueAdress, 4, rowNumber);
            Table_EventsData.Controls.Add(label_Status, 5, rowNumber);
            Table_EventsData.Controls.Add(label_SmailTemplate, 6, rowNumber);
            Table_EventsData.Controls.Add(button_Edit, 7, rowNumber);
        }
        public void editEventTableRow(Event eventEntity, int rowNumber)
        {
            Table_EventsData.GetControlFromPosition(0, rowNumber).Text = 
                eventEntity.eventName;

            Table_EventsData.GetControlFromPosition(1, rowNumber).Text = 
                eventEntity.date_From.ToString("yyyy/MM/dd");

            Table_EventsData.GetControlFromPosition(2, rowNumber).Text = 
                eventEntity.eventLengthDays.ToString();

            Table_EventsData.GetControlFromPosition(3, rowNumber).Text = 
                eventEntity.venueName;

            Table_EventsData.GetControlFromPosition(4, rowNumber).Text = 
                eventEntity.venueAdress; 

            Table_EventsData.GetControlFromPosition(5, rowNumber).Text = 
                eventEntity.eventStatus; 

            Table_EventsData.GetControlFromPosition(6, rowNumber).Text = 
                eventEntity.current_Mail_Template;

            events[rowNumber] = eventEntity;
        }
        private void ButtonEditEventHandler(object sender, EventArgs e)
        {
            var cellPos = GetRowColIndex(
            Table_EventsData,
            Table_EventsData.PointToClient(Cursor.Position));
            EditEvent editEvent = new EditEvent(this, events[cellPos.Value.Y].id);
            editEvent.Show();
            this.Enabled = false;
        }

        //Main Window Refistration tab actions

        public void UpdateParticipantsTable()
        {

            
            Table_ParticipantData.RowCount = participants.Count;
            for (int i = 0; i < participants.Count - 1; i++)
            {
                Table_ParticipantData.RowStyles.Add(new RowStyle(SizeType.Absolute));
            }
            TableLayoutRowStyleCollection styles =
            Table_ParticipantData.RowStyles;
            foreach (RowStyle style in styles)
            {
                // Set the row height to 20 pixels.
                style.SizeType = SizeType.Absolute;
                style.Height = 40;
            }
            for (int i = 0; i < participants.Count; i++)
            {
                addParticipantToParticipantTableRow(participants[i], i);
            }

        }
        public void addParticipantToParticipantTableRow(Participant participant, int rowNumber)
        {
            int eventDaysParticipating = participantService.countInHowManyDaysParticipating(
                participant);
            double paymentAmountForEventDay = events.Find(delegate (Event eventValue)
            {
                return eventValue.id == long.Parse(participant.eventId);
            }).paymentAmountForDay;

            double paymentAmount = participantService.countPaymentAmount(
                paymentAmountForEventDay, eventDaysParticipating);

            Label label_FirstName = new Label();
            label_FirstName.Text = participant.firstName;
            label_FirstName.Width = 300;
            Label label_LastName = new Label();
            label_LastName.Text = participant.lastName;
            label_LastName.Width = 200;
            Label label_JobTitle = new Label();
            label_JobTitle.Text = participant.jobTitle;
            label_JobTitle.Width = 150;
            Label label_CompanyType = new Label();
            label_CompanyType.Text = participant.companyType;
            label_CompanyType.Width = 200;
            Label label_CompanyName = new Label();
            label_CompanyName.Text = participant.companyName;
            label_CompanyName.Width = 200;
            Label label_ParticipationFormat = new Label();
            label_ParticipationFormat.Text = participant.participationFormat;
            label_ParticipationFormat.Width = 160;
            Label label_PaymentStatus = new Label();
            label_PaymentStatus.Text = participant.paymentStatus;
            label_PaymentStatus.Width = 320;
            Label label_PaymentAmount = new Label();
            label_PaymentAmount.Text = paymentAmount.ToString();
            label_PaymentAmount.Width = 320;
            Label label_Email = new Label();
            label_Email.Text = participant.email;
            label_Email.Width = 320;
            Label label_PhoneNumber = new Label();
            label_PhoneNumber.Text = participant.phoneNumber;
            label_PhoneNumber.Width = 320;
            Label label_TicketSent = new Label();
            label_TicketSent.Text = participant.ticketSent ? "yes" : "no";
            label_TicketSent.Width = 320;

            MetroButton button_Edit = new MetroButton();
            button_Edit.Height = 40;
            button_Edit.Width = 80;
            button_Edit.Text = "Edit";
            button_Edit.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            button_Edit.BackColor = ColorTranslator.FromHtml("#626262");
            button_Edit.ForeColor = Color.White;
            button_Edit.UseCustomBackColor = true;
            button_Edit.UseCustomForeColor = true;
            button_Edit.MouseClick += ButtonEditParticipantHandler;


            Table_ParticipantData.Controls.Add(label_FirstName, 0, rowNumber);
            Table_ParticipantData.Controls.Add(label_LastName, 1, rowNumber);
            Table_ParticipantData.Controls.Add(label_JobTitle, 2, rowNumber);
            Table_ParticipantData.Controls.Add(label_CompanyType, 3, rowNumber);
            Table_ParticipantData.Controls.Add(label_CompanyName, 4, rowNumber);
            Table_ParticipantData.Controls.Add(label_ParticipationFormat, 5, rowNumber);
            Table_ParticipantData.Controls.Add(label_PaymentStatus, 6, rowNumber);
            Table_ParticipantData.Controls.Add(label_PaymentAmount, 7, rowNumber);
            Table_ParticipantData.Controls.Add(label_Email, 8, rowNumber);
            Table_ParticipantData.Controls.Add(label_PhoneNumber, 9, rowNumber);
            Table_ParticipantData.Controls.Add(label_TicketSent, 10, rowNumber);
            Table_ParticipantData.Controls.Add(button_Edit, 11, rowNumber);
        }
        private void Button_RegisterParticipant_Click(object sender, EventArgs e)
        {
            RegisterParticipant cEvent = new RegisterParticipant(this,events);
            this.Enabled = false;
            cEvent.Show();
        }
        private void ButtonEditParticipantHandler(object sender, EventArgs e)
        {
            var cellPos = GetRowColIndex(
            Table_ParticipantData,
            Table_ParticipantData.PointToClient(Cursor.Position));
            EditParticipant editParticipant = new EditParticipant(this, 
                participants[cellPos.Value.Y],events, cellPos.Value.Y);
            editParticipant.Show();
            this.Enabled = false;
        }
        public void addParticipantTableRow()
        {
            this.addTableRow(Table_ParticipantData);
        }

        public void editParticipantTableRow(Participant participant, int rowNumber)
        {
            int eventDaysParticipating = participantService.countInHowManyDaysParticipating(
                participant);
            double paymentAmountForEventDay = events.Find(delegate (Event eventValue)
            {
                return eventValue.id == long.Parse(participant.eventId);
            }).paymentAmountForDay;

            double paymentAmount = participantService.countPaymentAmount(
                paymentAmountForEventDay, eventDaysParticipating);
            Table_ParticipantData.GetControlFromPosition(0, rowNumber).Text =
                participant.firstName;

            Table_ParticipantData.GetControlFromPosition(1, rowNumber).Text =
                participant.lastName;

            Table_ParticipantData.GetControlFromPosition(2, rowNumber).Text =
                participant.jobTitle;

            Table_ParticipantData.GetControlFromPosition(3, rowNumber).Text =
                participant.companyType;

            Table_ParticipantData.GetControlFromPosition(4, rowNumber).Text =
                participant.companyName;

            Table_ParticipantData.GetControlFromPosition(5, rowNumber).Text =
                participant.participationFormat;

            Table_ParticipantData.GetControlFromPosition(6, rowNumber).Text =
                participant.paymentStatus;

            Table_ParticipantData.GetControlFromPosition(7, rowNumber).Text =
                paymentAmount.ToString();

            Table_ParticipantData.GetControlFromPosition(8, rowNumber).Text =
                participant.email;

            Table_ParticipantData.GetControlFromPosition(9, rowNumber).Text =
                participant.phoneNumber;

            Table_ParticipantData.GetControlFromPosition(10, rowNumber).Text =
                participant.ticketSent ? "yes":"no";

            participants[rowNumber] = participant;
        }

        protected void ClosedHandler(object sender, EventArgs e)
        {
            login.Show();
        }

        private void Button_GenerateMail_Click(object sender, EventArgs e)
        {
            GenerateSend generateSend = new GenerateSend(this);
            generateSend.Show();
            this.Enabled = false;
        }

        private void Button_AddUser_Click(object sender, EventArgs e)
        {
            CreateUser user = new CreateUser(this);
            user.Show();
            this.Enabled = false;
        }

        private void Button_GenerateTicket_Click(object sender, EventArgs e)
        {
            GenerateTicket ticket = new GenerateTicket(this);
            ticket.Show();
            this.Enabled = false;
        }

        private void Button_EditEmail_Click(object sender, EventArgs e)
        {
            EditEmailTemplate email = new EditEmailTemplate(this);
            email.Show();
            this.Enabled = false;
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            GenerateExcel generator = new GenerateExcel();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            generator.ExportEventInfo("Event Name", projectDirectory, "YeetForLife");
        }

        private void Button_ChangeInformation_Click(object sender, EventArgs e)
        {
            AddChangeCompanyInformation changeInformationWindow = 
                new AddChangeCompanyInformation();
            changeInformationWindow.Show();
        }

        public void addTableRow(TableLayoutPanel table)
        {
            table.RowCount = table.RowCount + 1;
            table.RowStyles.Add(new RowStyle(SizeType.Absolute));
            TableLayoutRowStyleCollection styles =
            table.RowStyles;
            RowStyle styleRow = styles[table.RowCount - 1];

            // Set the row height to 20 pixels.
            styleRow.SizeType = SizeType.Absolute;
            styleRow.Height = 40;
        }
        //get collumn and row of clicked table edit button
        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new Point(col, row);
        }
    }
}
