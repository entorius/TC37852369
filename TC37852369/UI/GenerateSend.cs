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
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.Services.EmailSending;
using TC37852369.Services.Ticket_generation;
using TC37852369.UI;
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class GenerateSend : MetroForm
    {
        MainWindow mainWindow;
        List<Event> events = new List<Event>();
        List<Participant> participants = new List<Participant>();
        List<Participant> filteredParticipants = new List<Participant>();
        List<Participant> checkedParticipants = new List<Participant>();
        List<CheckBox> SendCheckBoxes = new List<CheckBox>();
        List<EmailTemplate> emailTemplates;
        TicketCreation ticketCreation = new TicketCreation();
        SendEmail sendEmail;
        CompanyData companyData;
        EmailStringHelper emailStringHelper = new EmailStringHelper();
        MetroMessageBoxHelper metroMessageBoxHelper = new MetroMessageBoxHelper();
        ParticipantServices participantServices = new ParticipantServices();
        GenerateSendInfoWindow generateSendInfoWindow;
        public CancellationTokenSource cancelationTokenSource;
        ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
        List<ParticipationFormat> participationFormats = new List<ParticipationFormat>();

        public string sendingStatus = "";
        

        public GenerateSend(MainWindow window, List<Event> events,List<Participant> participants
            ,CompanyData companyData, List<EmailTemplate> emailTemplates)
        {
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            this.events = events;
            this.participants = participants;
            this.companyData = companyData;
            this.emailTemplates = emailTemplates;
            sendEmail = new SendEmail(companyData.emailUsername, companyData.emailPassword);
            FillWindowFields();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        async void FillWindowFields()
        {
            participationFormats = await participationFormatServices.getAllParticipationFormats();
            foreach(ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }

            for (int i = 0; i < events.Count; i++)
            {
                ComboBox_Events.Items.Add(events[i].eventName);
            }
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus);
            }

            ComboBox_EmailSent.Items.Add("Any");
            foreach (YesNo yesno in (YesNo[])Enum.GetValues(typeof(YesNo)))
            {
                ComboBox_EmailSent.Items.Add(yesno);
            }
            ComboBox_EmailSent.SelectedIndex = 0;
            

        }

        private void Button_Send_Click(object sender, EventArgs e)
        {
            if (companyData.emailUsername.Length == 0 || companyData.emailUsername.Length == 0)
            {
                metroMessageBoxHelper.showWarning(this, "You cannot send email because you have not entered " +
                    "your email credentials, which are mandatory to send email. Go to Main Window -> Settings -> Add/" +
                    "Change company information to fill email credentials", "Warning");
            }
            else
            {
                for(int i = 0; i < SendCheckBoxes.Count; i++)
                {
                    if (SendCheckBoxes[i].Checked)
                    {
                        checkedParticipants.Add(filteredParticipants[i]);
                    }
                }
                cancelationTokenSource = new CancellationTokenSource();
                Button_Send.Enabled = false;
                generateSendInfoWindow = new GenerateSendInfoWindow(this);
                sendingStatus = "GeneratingDocuments";
                Timer_StringChanged.Enabled = true;
               
                generateSendInfoWindow.Show();
                generateSendInfoWindow.BringToFront();

                var tasks = new[]
                {
                    Task.Factory.StartNew(() => GenerateDocumentsAndSendEmails(),cancelationTokenSource.Token)
                };

                //mainWindow.Enabled = true;
                //this.Dispose();
            }
        }

        private async void GenerateDocumentsAndSendEmails()
        {
            Dictionary<string,Event> eventsToSend = new Dictionary<string,Event>();
            foreach(Participant p in checkedParticipants)
            {
                Event eventToAdd = events.Find(ev => ev.id.ToString().Equals(p.eventId));
                if (!eventsToSend.ContainsKey(eventToAdd.eventName))
                {
                    eventsToSend.Add(eventToAdd.eventName, eventToAdd);
                }
            }
            List<Event> eventsToSendList = new List<Event>();
            foreach(KeyValuePair<string,Event> ev in eventsToSend)
            {
                eventsToSendList.Add(ev.Value);
            }


            List<string> ticketsPaths = await ticketCreation.generateTicketsAndSave(
                    checkedParticipants, eventsToSendList, companyData);
            sendingStatus = "SendingEmails";
            List<string> toEmails = new List<string>();
            List<string> emailBodies = new List<string>();
            List<string> emailSubjects = new List<string>();
            List<string> ticketsNames = new List<string>();
            foreach (Participant p in filteredParticipants)
            {
                toEmails.Add(p.email);
                Event eventEntity = events.Find(ev => ev.id.ToString().Equals(p.eventId));
                string body = "";
                string subject = "";
                if (eventEntity.useTemplate)
                {
                    EmailTemplate temp = emailTemplates.Find(et =>
                    et.templateName.Equals(eventEntity.current_Mail_Template));
                    body = temp.body;
                    subject = temp.subject;
                }
                else
                {
                    body = eventEntity.emailBody;
                    subject = eventEntity.emailSubject;
                }
                emailBodies.Add(emailStringHelper.formatEmailString(body, p, eventEntity));
                emailSubjects.Add(emailStringHelper.formatEmailString(subject, p, eventEntity));
                ticketsNames.Add(eventEntity.eventName + " ticket");
            }

            sendEmail.SendEmails(toEmails, emailSubjects, emailBodies, ticketsPaths, ticketsNames);
            sendingStatus = "EmailsSent";
            foreach (Participant p in filteredParticipants)
            {
                p.ticketSent = true;
                Participant participant = await participantServices.editParticipant(p);

                if (participant != null)
                {
                    int allParticipantsIndex = mainWindow.allParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                    int selectedEventParticipantsIndex = 0;
                    int filteredEventParticipantsIndex = 0;
                    if (mainWindow.selectedEventParticipants.Count > 0)
                    {
                        selectedEventParticipantsIndex = mainWindow.selectedEventParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                        filteredEventParticipantsIndex = mainWindow.filteredParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                    }
                    mainWindow.allParticipants[allParticipantsIndex] = p;
                    if (mainWindow.selectedEventParticipants.Count > 0)
                    {
                        mainWindow.selectedEventParticipants[selectedEventParticipantsIndex] = p;
                        mainWindow.filteredParticipants[filteredEventParticipantsIndex] = p;
                        mainWindow.editParticipantTableRow(participant, filteredEventParticipantsIndex);
                    }
                }
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

        private void Button_Filter_Click(object sender, EventArgs e)
        {
            filteredParticipants = filterParticipants();
            Table_FilteredResults.SuspendLayout();
            emptyTable(Table_FilteredResults);
            SendCheckBoxes.Clear();
            for (int i = 0; i < filteredParticipants.Count; i++)
            {
                addParticipantToParticipantTableRow(filteredParticipants[i], i, Table_FilteredResults);
                addTableRow(Table_FilteredResults);
            }
            Table_FilteredResults.ResumeLayout();
        }


        private List<Participant> filterParticipants()
        {
            
            List<Participant> filteredParticipants = participants;

            if (ComboBox_Events.SelectedIndex >= 0)
            {
                filteredParticipants = filterAccordingToEvent(filteredParticipants, events[ComboBox_Events.SelectedIndex]);
            }
            if (ComboBox_EmailSent.SelectedIndex != 0)
            {
                filteredParticipants = filterAccordingToEmailSent(filteredParticipants, ComboBox_EmailSent.SelectedIndex == 1);
            }
            if (TextBox_CompanyName.Text.Length > 0)
            {
                filteredParticipants = filterAccordingToComapnyName(filteredParticipants, TextBox_CompanyName.Text);
            }
            if (ComboBox_PaymentStatus.SelectedIndex >= 0)
            {
                filteredParticipants = filterAccordingToPaid(filteredParticipants, ComboBox_PaymentStatus.SelectedItem.ToString());
            }
            if (TextBox_FirstName.Text.Length > 0)
            {
                filteredParticipants = filterAccordingToFirstName(filteredParticipants, TextBox_FirstName.Text);
            }
            if (TextBox_LastName.Text.Length > 0)
            {
                filteredParticipants = filterAccordingToLastName(filteredParticipants, TextBox_LastName.Text);
            }
            if (ComboBox_ParticipationFormat.SelectedIndex >= 0)
            {
                filteredParticipants = filterAccordingToParticipationFormat(filteredParticipants, participationFormats[ComboBox_ParticipationFormat.SelectedIndex]);
            }
            return filteredParticipants;
        }

        private List<Participant> filterAccordingToParticipationFormat(List<Participant> filteredParticipants, ParticipationFormat participationFormat)
        {
            return filteredParticipants.FindAll(p => p.participationFormat.Equals(participationFormat.Value));
        }

        public List<Participant> filterAccordingToEvent(List<Participant> participants, Event eventEntity)
        {
            List<Participant> filteredParticipantEvent = new List<Participant>();

            foreach(Participant participant in participants)
            {
                if (participant.eventId.Equals(eventEntity.id.ToString()))
                {
                    filteredParticipantEvent.Add(participant);
                }
            }
            return filteredParticipantEvent;
        }
        public List<Participant> filterAccordingToEmailSent(List<Participant> participants, bool emailSent)
        {
            List<Participant> filteredParticipantEmailSent = new List<Participant>();

            foreach (Participant participant in participants)
            {
                if (participant.ticketSent == emailSent)
                {
                    filteredParticipantEmailSent.Add(participant);
                }
            }
            return filteredParticipantEmailSent;
        }
        public List<Participant> filterAccordingToComapnyName(List<Participant> participants, string companyName)
        {
            List<Participant> filteredParticipantComapanyName = new List<Participant>();

            foreach (Participant participant in participants)
            {
                if (participant.companyName.ToLower().Equals(companyName.ToLower()))
                {
                    filteredParticipantComapanyName.Add(participant);
                }
            }
            return filteredParticipantComapanyName;
        }
        public List<Participant> filterAccordingToPaid(List<Participant> participants, string paid)
        {
            List<Participant> filteredParticipantPaid = new List<Participant>();

            foreach (Participant participant in participants)
            {
                if (participant.paymentStatus.Equals(paid))
                {
                    filteredParticipantPaid.Add(participant);
                }
            }
            return filteredParticipantPaid;
        }
        public List<Participant> filterAccordingToFirstName(List<Participant> participants, string firstName)
        {
            List<Participant> filteredParticipantFirstName = new List<Participant>();

            foreach (Participant participant in participants)
            {
                if (participant.firstName.ToLower().Contains(firstName.ToLower()))
                {
                    filteredParticipantFirstName.Add(participant);
                }
            }
            return filteredParticipantFirstName;
        }
        public List<Participant> filterAccordingToLastName(List<Participant> participants, string lastName)
        {
            List<Participant> filteredParticipantLastName = new List<Participant>();

            foreach (Participant participant in participants)
            {
                if (participant.lastName.ToLower().Contains(lastName.ToLower()))
                {
                    filteredParticipantLastName.Add(participant);
                }
            }
            return filteredParticipantLastName;
        }
        public void addParticipantToParticipantTableRow(Participant participant, int rowNumber, TableLayoutPanel table)
        {
            Label label_FirstName = new Label();
            label_FirstName.Text = participant.firstName;
            label_FirstName.Width = 300;
            Label label_LastName = new Label();
            label_LastName.Text = participant.lastName;
            label_LastName.Width = 200;
            Label label_CompanyName = new Label();
            label_CompanyName.Text = participant.companyName;
            label_CompanyName.Width = 200;
            Label label_PaymentStatus = new Label();
            label_PaymentStatus.Text = participant.paymentStatus;
            label_PaymentStatus.Width = 320;
            Label label_Email = new Label();
            label_Email.Text = participant.email;
            label_Email.Width = 320;
            Label label_TicketSent = new Label();
            label_TicketSent.Text = participant.ticketSent ? "yes" : "no";
            label_TicketSent.Width = 320;

            CheckBox button_Check = new CheckBox();
            button_Check.Checked = false;
            button_Check.Visible = true;
            SendCheckBoxes.Add(button_Check);

            table.Controls.Add(label_FirstName,      0, rowNumber);
            table.Controls.Add(label_LastName,       1, rowNumber);
            table.Controls.Add(label_CompanyName,    2, rowNumber);
            table.Controls.Add(label_PaymentStatus,  3, rowNumber);
            table.Controls.Add(label_Email,          4, rowNumber);
            table.Controls.Add(label_TicketSent,     5, rowNumber);
            table.Controls.Add(button_Check,         6, rowNumber);
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
        public void emptyTable(TableLayoutPanel table)
        {
            table.Controls.Clear();
            table.RowCount = 1;
        }

        private void Timer_StringChanged_Tick(object sender, EventArgs e)
        {
            if (sendingStatus.Equals("GeneratingDocuments"))
            {
                generateSendInfoWindow.Label_Status.Text = "Generating Tickets For  Events";
            }
            else if (sendingStatus.Equals("SendingEmails"))
            {
                generateSendInfoWindow.Timer_Document.Enabled = false;
                generateSendInfoWindow.Timer_Sending.Enabled = true;
                generateSendInfoWindow.Label_Status.Text = "Sending Emails To Participants";
            }
            else if (sendingStatus.Equals("EmailsSent"))
            {
                generateSendInfoWindow.Timer_Sending.Enabled = false;
                generateSendInfoWindow.Timer_Sent.Enabled = true;
                generateSendInfoWindow.Button_Confirm.Enabled = true;
                generateSendInfoWindow.Button_Cancel.Enabled = false;
                generateSendInfoWindow.Label_Status.Text = "Emails    Sent    Successfully";
            }
        }

        private void CheckBox_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_CheckAll.Checked)
            {
                foreach(CheckBox checkBox in SendCheckBoxes)
                {
                    checkBox.Checked = true;
                }
            }
            else
            {
                foreach (CheckBox checkBox in SendCheckBoxes)
                {
                    checkBox.Checked = false;
                }
            }
        }
    }
}
