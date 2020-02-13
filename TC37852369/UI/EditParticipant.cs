using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
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
    public partial class EditParticipant : MetroForm
    {
        MainWindow mainWindow;
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        TableLayoutHelper tableLayoutHelper = new TableLayoutHelper();
        EnumHelper enumHelper = new EnumHelper();
        Event participantEvent;
        Participant participant;
        ParticipantServices participantServices = new ParticipantServices();
        ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
        public List<ParticipationFormat> participationFormats = new List<ParticipationFormat>();
        MailTemplateServices emailTemplateServices = new MailTemplateServices();
        EmailStringHelper emailStringHelper = new EmailStringHelper();
        List<EmailTemplate> emailTemplates;
        public CancellationTokenSource cancelationTokenSource;
        GenerateSendInfoWindow generateSendInfoWindow;
        CompanyData companyData;
        TicketCreation ticketCreation = new TicketCreation();
        string sendingStatus;
        int rowNumber;
        SendEmail sendEmail;

        string addNewParticipationFormat = "+ Add new participation format";
        string deleteParticipationFormat = "x Delete participation format";
        bool addNewParticipantFormatSelected = false;
        bool deleteParticipantFormatSelected = false;
        public EditParticipant(MainWindow window, Participant participant,List<Event> events,CompanyData companyData,int rowNumber)
        {
            this.rowNumber = rowNumber;
            this.participant = participant;
            this.companyData = companyData;
            
            sendEmail = new SendEmail(companyData.emailUsername, companyData.emailPassword);
            participantEvent = events.FindLast(
                 delegate (Event e)
                 {
                     return e.id == long.Parse(participant.eventId);
                 }
                 );
            mainWindow = window;
            InitializeComponent();
            this.FormClosed += CloseHandler;

            loadWindowData();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private async void loadWindowData()
        {
            this.emailTemplates = await emailTemplateServices.getAllMailTemplates();
            participationFormats = await participationFormatServices.getAllParticipationFormats();

            foreach (ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }
            EventDaysShowHide(participantEvent.eventLengthDays);
            Label_EventName.Text = participantEvent.eventName;
            foreach (CompanyTypes companyType in (CompanyTypes[])Enum.GetValues(typeof(CompanyTypes)))
            {
                ComboBox_CompanyType.Items.Add(companyType);
            }

            //Set payment status values to comboBox and select first item
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus);
            }
            ComboBox_PaymentStatus.SelectedIndex = ComboBox_PaymentStatus.Items.IndexOf(participant.paymentStatus);

            

            //Set Yes No values to materials, participation in evening event, and participation 
            //in event days comboBoxes and select no values for all exept for participating in 
            //first day of event
            foreach (YesNo yesNo in (YesNo[])Enum.GetValues(typeof(YesNo)))
            {
                ComboBox_Materials.Items.Add(yesNo);
                ComboBox_ParticipateEveningEvent.Items.Add(yesNo);
                ComboBox_ParticipateDay1.Items.Add(yesNo);
                ComboBox_ParticipateDay2.Items.Add(yesNo);
                ComboBox_ParticipateDay3.Items.Add(yesNo);
                ComboBox_ParticipateDay4.Items.Add(yesNo);
            }

            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.Items.Add(deleteParticipationFormat);
            ComboBox_ParticipationFormat.SelectedIndexChanged += ParticipationFormatSelectedIndexChanged;
            DateTime_PaymentDate.Value = participant.paymentDate;
            DateTime_RegistrationDate.Value = participant.registrationDate;

            

            if (participant.paymentAmount > 0)
            {
                TextBox_PaymentAmount.Text = participant.paymentAmount.ToString();
            }
            else
            {
                TextBox_PaymentAmount.Text = "";
            }

            if(participant.additionalPhoneNumber.Length > 0)
            {
                TextBox_AdditionalPhoneNumber.Text = participant.additionalPhoneNumber;
            }
            else
            {
                PictureBox_AddAdditionalPhoneNumber.Show();
                PictureBox_DeleteAdditionalPhoneNumber.Hide();
                TextBox_AdditionalPhoneNumber.Text = "";
                TextBox_AdditionalPhoneNumber.Hide();
                Label_AdditionalPhoneNumber.Hide();
            }
            showHidePyamentAmountAndDate();
            TextBox_Comment.Text = participant.comment;

            setFieldsData();
        }

            protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            Button_Save.Enabled = false;
            Button_Send.Enabled = false;
            Button_Delete.Enabled = false;
            int partcipantInformationManditoryFieldsFilled =
                participantServices.isPartcipantInformationManditoryFieldsCorrect(
                    TextBox_FirstName.Text, TextBox_LastName.Text, TextBox_Email.Text
                    );
            bool paymentAmountCorrect = participantServices.paymentAmountStringCorrect(TextBox_PaymentAmount.Text);
            if (partcipantInformationManditoryFieldsFilled > 0 || !paymentAmountCorrect)
            {
                if (partcipantInformationManditoryFieldsFilled == 1)
                {
                    messageBoxHelper.showWarning(this,
                        "First Name field text is too short (not correct name entered " +
                        "(is shorter than 3 characters) or name has not been entered)", "Warning");
                }
                else if (partcipantInformationManditoryFieldsFilled == 2)
                {
                    messageBoxHelper.showWarning(this,
                        "Last Name field text is too short (not correct last name entered " +
                        "(is shorter than 3 characters) or last name has not been entered)", "Warning");
                }
                else if (partcipantInformationManditoryFieldsFilled == 3)
                {
                    messageBoxHelper.showWarning(this,
                        "Email field text is not correct (not correct email format " +
                        "(must be: (some cheracters)@(domain) etc. example@gmail.com)" +
                        " or email has not been entered)", "Warning");
                }
                else if (!paymentAmountCorrect)
                {
                    messageBoxHelper.showWarning(this,
                      "Payment amount is negative or not a number", "Warning");
                }
            }
            else
            {
                double paymentAmount;
                Double.TryParse(TextBox_PaymentAmount.Text, out paymentAmount);
                if (paymentAmount == 0)
                {
                    paymentAmount = -1;
                }
                Participant createdParticipant = new Participant(
                participant.participantId,
                participant.eventId,
                TextBox_FirstName.Text,
                TextBox_LastName.Text,
                TextBox_JobTitle.Text,
                TextBox_CompanyName.Text,
                ComboBox_CompanyType.Text,
                TextBox_Email.Text,
                TextBox_PhoneNumber.Text,
                TextBox_Country.Text,
                ComboBox_ParticipationFormat.Text,
                ComboBox_PaymentStatus.Text,
                ComboBox_Materials.Text.Equals("Yes") ? true : false,
                participant.ticketBarcode,
                participant.ticketSent,
                ComboBox_ParticipateEveningEvent.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay1.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay2.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay3.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay4.Text.Equals("Yes") ? true : false,
                participant.checkedInDay1,
                participant.checkedInDay2,
                participant.checkedInDay3,
                participant.checkedInDay4,
                DateHelper.setDateToMidnight(DateTime_RegistrationDate.Value),
                DateHelper.setDateToMidnight(DateTime_PaymentDate.Value),
                paymentAmount,
                TextBox_AdditionalPhoneNumber.Text,
                TextBox_Comment.Text
                );
                bool editedSuccesfully = true;
                try
                {
                    await participantServices.editParticipant(createdParticipant);
                }
                catch (Exception)
                {
                    editedSuccesfully = false;
                }
                if (createdParticipant != null && mainWindow.selectedEvent != null && editedSuccesfully)
                {

                    mainWindow.Enabled = true;
                    int participantRow = this.mainWindow.filteredParticipants.FindLastIndex(delegate (Participant participant)
                    {
                        return participant.participantId == createdParticipant.participantId;
                    });
                    mainWindow.editParticipantTableRow(createdParticipant, participantRow);
                    this.Dispose();
                }
                else
                {
                    messageBoxHelper.showWarning(this, "Event creation was unsuccesfull, because of internet connection" +
                        "or database write request number exceeded", "Warning");
                }
            }
            Button_Save.Enabled = true;
            Button_Send.Enabled = true;
            Button_Delete.Enabled = true;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }

        private async void Button_Delete_Click(object sender, EventArgs e)
        {
            DialogResult result= messageBoxHelper.showYesNo(this, 
                "Are you sure you want to delete this participant", "Question");
            if(result == DialogResult.Yes)
            {
                bool deleted = await participantServices.deleteParticipant(participant.participantId);

                if (deleted)
                {
                    tableLayoutHelper.RemoveArbitraryRow(mainWindow.Table_ParticipantsData1, rowNumber);
                    mainWindow.participantsToolTipHelpers.RemoveAt(rowNumber);
                    mainWindow.allParticipants.Remove(participant);
                    mainWindow.filteredParticipants.Remove(participant);
                    int id = mainWindow.selectedEventParticipants.FindIndex(p => p.participantId.Equals(participant.participantId));
                    mainWindow.selectedEventParticipants.RemoveAt(id);
                    mainWindow.Enabled = true;
                    this.Dispose();
                }
                else
                {
                    messageBoxHelper.showWarning(this, "Delete unsuccesful. Check your internet " +
                        "connection or contact programmer (might be database issues)","Warning");
                }
                
            }
            else if(result == DialogResult.No)
            {

            }
            
        }
        public void setFieldsData()
        {
            this.Label_EventName.Text = participantEvent.eventName;
            this.TextBox_FirstName.Text = participant.firstName;
            this.TextBox_LastName.Text = participant.lastName;
            this.TextBox_JobTitle.Text = participant.jobTitle;
            this.TextBox_CompanyName.Text = participant.companyName;
            
            this.ComboBox_CompanyType.SelectedIndex = enumHelper.GetStringEnumIndex(
                typeof(CompanyTypes), participant.companyType);
            this.TextBox_Email.Text = participant.email;
            this.TextBox_PhoneNumber.Text = participant.phoneNumber;
            this.TextBox_Country.Text = participant.country;
            this.ComboBox_ParticipationFormat.SelectedIndex = 
                this.ComboBox_ParticipationFormat.Items.IndexOf(participant.participationFormat);
            this.ComboBox_PaymentStatus.SelectedIndex = enumHelper.GetStringEnumIndex(
                typeof(PaymentStatus), participant.paymentStatus); ;
            this.ComboBox_Materials.SelectedIndex = participant.materials ? 0 : 1;
            this.ComboBox_ParticipateEveningEvent.SelectedIndex = 
                participant.participateEveningEvent ? 0 : 1;
            this.ComboBox_ParticipateDay1.SelectedIndex = participant.participateInDay1 ? 0 : 1;
            this.ComboBox_ParticipateDay2.SelectedIndex = participant.participateInDay2 ? 0 : 1;
            this.ComboBox_ParticipateDay3.SelectedIndex = participant.participateInDay3 ? 0 : 1;
            this.ComboBox_ParticipateDay4.SelectedIndex = participant.participateInDay4 ? 0 : 1;
        }
        
        private void ParticipationFormatSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.ComboBox_ParticipationFormat.SelectedIndex == this.ComboBox_ParticipationFormat.Items.Count - 1 && !addNewParticipantFormatSelected)
            {
                Console.WriteLine("Yahooo");
                this.ComboBox_ParticipationFormat.SelectedIndex = -1;
                this.Enabled = false;
                MetroForm registerParticipationFormat = new RegisterParticipationString(this);
                registerParticipationFormat.Show();
                registerParticipationFormat.Disposed += AddParticipationFormats;
                addNewParticipantFormatSelected = true;
            }
            else if (this.ComboBox_ParticipationFormat.SelectedIndex == this.ComboBox_ParticipationFormat.Items.Count - 1 && !deleteParticipantFormatSelected)
            {
                Console.WriteLine("Yahooo");
                this.ComboBox_ParticipationFormat.SelectedIndex = -1;
                this.Enabled = false;
                MetroForm DeleteParticipationFormat = new DeleteParticipantionFormat(this, participationFormats);
                DeleteParticipationFormat.Show();
                DeleteParticipationFormat.Disposed += DeleteParticipationFormats;
                deleteParticipantFormatSelected = true;

            }
        }
        private void AddParticipationFormats(Object sender, EventArgs e)
        {
            ComboBox_ParticipationFormat.Items.Clear();
            foreach (ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }
            ComboBox_ParticipationFormat.SelectedIndex = ComboBox_ParticipationFormat.Items.Count - 1;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            addNewParticipantFormatSelected = false;
        }
        private void DeleteParticipationFormats(Object sender, EventArgs e)
        {
            ComboBox_ParticipationFormat.Items.Clear();
            foreach (ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }
            ComboBox_ParticipationFormat.SelectedIndex = -1;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.Items.Add(deleteParticipationFormat);
            addNewParticipantFormatSelected = false;
        }

        public void EventDaysShowHide(int eventDays)
        {
            eventDayShowHide(Label_ParticipateDay1, ComboBox_ParticipateDay1, 1, eventDays);
            eventDayShowHide(Label_ParticipateDay2, ComboBox_ParticipateDay2, 2, eventDays);
            eventDayShowHide(Label_ParticipateDay3, ComboBox_ParticipateDay3, 3, eventDays);
            eventDayShowHide(Label_ParticipateDay4, ComboBox_ParticipateDay4, 4, eventDays);
        }
        public void eventDayShowHide(MetroLabel eventDayLabel, MetroComboBox eventDayComboBox, int dayValueToCompare,
            int eventDays)
        {
                if (eventDays >= dayValueToCompare)
                {
                    eventDayLabel.Show();
                    eventDayComboBox.Show();
                }
                else
                {
                    eventDayLabel.Hide();
                    eventDayComboBox.Hide();
                }
        }

        private void Button_SendEmail_Click(object sender, EventArgs e)
        {
            if (mainWindow.companyData.emailUsername.Length == 0 || mainWindow.companyData.emailUsername.Length == 0)
            {
                messageBoxHelper.showWarning(this, "You cannot send email because you have not entered " +
                    "your email credentials, which are mandatory to send email. Go to Main Window -> Settings -> Add/" +
                    "Change company information to fill email credentials", "Warning");
            }
            else
            {
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
            List<Participant> participants = new List<Participant>();
            participants.Add(participant);
            List<Event> events = new List<Event>();
            events.Add(participantEvent);

            List<string> ticketsPaths = await ticketCreation.generateTicketsAndSave(
                    participants, events, companyData);
            sendingStatus = "SendingEmails";
            List<string> toEmails = new List<string>();
            List<string> emailBodies = new List<string>();
            List<string> emailSubjects = new List<string>();
            List<string> ticketsNames = new List<string>();
            foreach (Participant p in participants)
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
            foreach (Participant p in participants)
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

        private void PictureBox_AddAdditionalPhoneNumber_Click(object sender, EventArgs e)
        {
            PictureBox_AddAdditionalPhoneNumber.Hide();
            PictureBox_DeleteAdditionalPhoneNumber.Show();
            TextBox_AdditionalPhoneNumber.Show();
            Label_AdditionalPhoneNumber.Show();
        }

        private void PictureBox_DeleteAdditionalPhoneNumber_Click(object sender, EventArgs e)
        {
            PictureBox_AddAdditionalPhoneNumber.Show();
            PictureBox_DeleteAdditionalPhoneNumber.Hide();
            TextBox_AdditionalPhoneNumber.Text = "";
            TextBox_AdditionalPhoneNumber.Hide();
            Label_AdditionalPhoneNumber.Hide();
        }

        private void ComboBox_PaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            showHidePyamentAmountAndDate();
        }
        private void showHidePyamentAmountAndDate()
        {
            if (ComboBox_PaymentStatus.SelectedIndex == 2)
            {
                TextBox_PaymentAmount.Hide();
                Label_PaymentAmount.Hide();
            }
            else
            {
                TextBox_PaymentAmount.Show();
                Label_PaymentAmount.Show();
            }
            if (ComboBox_PaymentStatus.SelectedIndex == 1)
            {
                DateTime_PaymentDate.Show();
                Label_PaymentDate.Show();
            }
            else
            {
                DateTime_PaymentDate.Hide();
                Label_PaymentDate.Hide();
            }
        }
    }
}
