using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI;
using TC37852369.UI.helpers;

public enum CompanyTypes
{
    Industry,
    Service,
    Media,
    Government
}
//Delete when all participation formats will be added to the database

public enum PaymentStatus
{
   Due,                     //not paid for event
   Paid,                    //paid for event
   Free                    //event is free for this participant
}

public enum YesNo
{
    Yes,
    No
}


namespace TC37852369
{
    public partial class RegisterParticipant : MetroForm
    {
        MainWindow mainWindow;
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        List<Event> events = new List<Event>();
        ParticipantServices participantServices = new ParticipantServices();
        ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
        ParticipantFilteringServices participantFilteringServices = new ParticipantFilteringServices();
        public List<ParticipationFormat> participationFormats = new List<ParticipationFormat>();

        Participant templateParticipat;
        Event templateParticipatEvent;
        bool addParticipantDataAction = false;

        string addNewParticipationFormat = "+ Add new participation format";
        string deleteParticipationFormat = "x Delete participation format";
        bool addNewParticipantFormatSelected = false;
        bool deleteParticipantFormatSelected = false;
        bool paymentDateValueChanged = false;
        DateTime paymentValueDate = DateTime.MinValue;
        public RegisterParticipant(MainWindow window, List<Event> events)
        {
            mainWindow = window;
            this.events = events;
            this.FormClosed += CloseHandler;

            InitializeComponent();
            EventDaysShowHide(null);
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }

           loadWindowData();
            BringToFront();
        }
        public RegisterParticipant(MainWindow window, List<Event> events,Participant participant,Event eventEntity)
        {
            mainWindow = window;
            this.events = events;
            this.FormClosed += CloseHandler;

            InitializeComponent();
            EventDaysShowHide(null);
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            addParticipantDataAction = true;
            templateParticipat = participant;
            templateParticipatEvent = eventEntity;
            loadWindowData();
            BringToFront();
        }

        private bool addParticipantData()
        {
            ComboBox_Events.SelectedIndex = mainWindow.events.FindIndex(ev => ev.eventName.Equals(templateParticipatEvent.eventName));
            //participant.TextBox_FirstName = this.TextBox_FirstName;
            //participant.TextBox_LastName = this.TextBox_LastName;
            TextBox_JobTitle.Text = templateParticipat.jobTitle;
            TextBox_CompanyName.Text = templateParticipat.companyName;
            ComboBox_CompanyType.SelectedIndex = ComboBox_CompanyType.Items.IndexOf(templateParticipat.companyType);
            TextBox_Email.Text = templateParticipat.email;
            TextBox_PhoneNumber.Text = templateParticipat.phoneNumber;
            if (templateParticipat.additionalPhoneNumber.Length > 0)
            {
                TextBox_AdditionalPhoneNumber.Text = templateParticipat.additionalPhoneNumber;
                PictureBox_AddAdditionalPhoneNumber.Hide();
                Label_AdditionalPhoneNumber.Show();
                TextBox_AdditionalPhoneNumber.Show();
                PictureBox_DeleteAdditionalPhoneNumber.Show();
            }
            else
            {
                Label_AdditionalPhoneNumber.Hide();
                TextBox_AdditionalPhoneNumber.Hide();
                PictureBox_DeleteAdditionalPhoneNumber.Hide();
            }
            TextBox_Country.Text = templateParticipat.country;
            TextBox_Comment.Text = templateParticipat.comment;
            DateTime_PaymentDate.Value = templateParticipat.paymentDate;
            DateTime_RegistrationDate.Value = templateParticipat.registrationDate;
            TextBox_PaymentAmount.Text = templateParticipat.paymentAmount.ToString();
            ComboBox_ParticipationFormat.SelectedIndex = ComboBox_ParticipationFormat.Items.IndexOf(templateParticipat.participationFormat);
            ComboBox_PaymentStatus.SelectedIndex = ComboBox_PaymentStatus.Items.IndexOf(templateParticipat.paymentStatus);
            ComboBox_Materials.SelectedIndex = ComboBox_Materials.Items.IndexOf(templateParticipat.materials ? "Yes":"No");
            ComboBox_ParticipateEveningEvent.SelectedIndex = ComboBox_ParticipateEveningEvent.Items.IndexOf(templateParticipat.participateEveningEvent ? "Yes" : "No");
            ComboBox_ParticipateDay1.SelectedIndex = ComboBox_ParticipateDay1.Items.IndexOf(templateParticipat.participateInDay1 ? "Yes" : "No");
            ComboBox_ParticipateDay2.SelectedIndex = ComboBox_ParticipateDay2.Items.IndexOf(templateParticipat.participateInDay2 ? "Yes" : "No");
            ComboBox_ParticipateDay3.SelectedIndex = ComboBox_ParticipateDay3.Items.IndexOf(templateParticipat.participateInDay3 ? "Yes" : "No");
            ComboBox_ParticipateDay4.SelectedIndex = ComboBox_ParticipateDay4.Items.IndexOf(templateParticipat.participateInDay4 ? "Yes" : "No");
            return true;
        }

        private async void loadWindowData()
        {
            participationFormats = await participationFormatServices.getAllParticipationFormats();

            PictureBox_DeleteAdditionalPhoneNumber.Hide();
            TextBox_AdditionalPhoneNumber.Text = "";
            TextBox_AdditionalPhoneNumber.Hide();
            Label_AdditionalPhoneNumber.Hide();
            

            foreach (Event eventEntity in events)
            {
                ComboBox_Events.Items.Add(eventEntity.eventName);
            }
            foreach (CompanyTypes companyType in (CompanyTypes[])Enum.GetValues(typeof(CompanyTypes)))
            {
                ComboBox_CompanyType.Items.Add(companyType.ToString());
            }


            foreach (ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }
            //Set payment status values to comboBox and select first item
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus.ToString());
            }
            ComboBox_PaymentStatus.SelectedIndex = 0;

            //Set Yes No values to materials, participation in evening event, and participation 
            //in event days comboBoxes and select no values for all exept for participating in 
            //first day of event
            foreach (YesNo yesNo in (YesNo[])Enum.GetValues(typeof(YesNo)))
            {
                ComboBox_Materials.Items.Add(yesNo.ToString());
                ComboBox_ParticipateEveningEvent.Items.Add(yesNo.ToString());
                ComboBox_ParticipateDay1.Items.Add(yesNo.ToString());
                ComboBox_ParticipateDay2.Items.Add(yesNo.ToString());
                ComboBox_ParticipateDay3.Items.Add(yesNo.ToString());
                ComboBox_ParticipateDay4.Items.Add(yesNo.ToString());
            }
            ComboBox_Materials.SelectedIndex = 1;
            ComboBox_ParticipateEveningEvent.SelectedIndex = 1;
            ComboBox_ParticipateDay1.SelectedIndex = 0;
            ComboBox_ParticipateDay2.SelectedIndex = 1;
            ComboBox_ParticipateDay3.SelectedIndex = 1;
            ComboBox_ParticipateDay4.SelectedIndex = 1;

            TextBox_PaymentAmount.Text = "0";

            ComboBox_PaymentStatus.SelectedIndex = 0;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.Items.Add(deleteParticipationFormat);
            ComboBox_ParticipationFormat.SelectedIndexChanged += ParticipationFormatSelectedIndexChanged;
            DateTime_PaymentDate.Hide();
            if (addParticipantDataAction)
            {
                bool added = addParticipantData();
                BringToFront();
            }
        }
        private async void Button_Register_Click(object sender, EventArgs e)
        {
            Button_Register.Enabled = false;
            bool eventChoosen = ComboBox_Events.SelectedItem == null ? false : true;
            bool paymentAmountCorrect = true;
            if (ComboBox_PaymentStatus.SelectedIndex != 2)
            {
                paymentAmountCorrect = participantServices.paymentAmountStringCorrect(TextBox_PaymentAmount.Text);
            }
            int partcipantInformationManditoryFieldsFilled =
                participantServices.isPartcipantInformationManditoryFieldsCorrect(
                    TextBox_FirstName.Text, TextBox_LastName.Text, TextBox_Email.Text
                    );
            if (partcipantInformationManditoryFieldsFilled > 0 || !eventChoosen || !paymentAmountCorrect)
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
                else if (!eventChoosen)
                {
                    messageBoxHelper.showWarning(this,
                       "You have not choosen event!", "Warning");
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
                bool paymentAmountParsed = Double.TryParse(TextBox_PaymentAmount.Text, out paymentAmount);
                if (ComboBox_PaymentStatus.SelectedIndex == 2 || !paymentAmountParsed)
                {
                    paymentAmount = 0;
                }

                DateTime paymentDate = DateTime_PaymentDate.Value;
                Participant createdParticipant = await participantServices.createParticipant(
                events[ComboBox_Events.SelectedIndex].id.ToString(),
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
                ComboBox_ParticipateEveningEvent.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay1.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay2.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay3.Text.Equals("Yes") ? true : false,
                ComboBox_ParticipateDay4.Text.Equals("Yes") ? true : false,
                DateHelper.setDateToMidnight(paymentDate),
                DateHelper.setDateToMidnight(DateTime_RegistrationDate.Value),
                paymentAmount,
                TextBox_AdditionalPhoneNumber.Text,
                TextBox_Comment.Text

                );
                if (createdParticipant != null)
                {
                    mainWindow.Enabled = true;
                    mainWindow.allParticipants.Add(createdParticipant);
                    if (mainWindow.selectedEvent != null)
                    {
                        if (createdParticipant.eventId.Equals(mainWindow.selectedEvent.id.ToString()))
                        {
                            List<Participant> currentParticipantList = new List<Participant>();
                            currentParticipantList.Add(createdParticipant);
                            List<Participant> filteredParticipant = participantFilteringServices.filterParticipants(currentParticipantList, mainWindow.filterWindowData, mainWindow.selectedEvent);
                            if (filteredParticipant.Count > 0)
                            {
                                mainWindow.filteredParticipants.Add(createdParticipant);
                               
                                if (mainWindow.selectedEvent != null)
                                {
                                    if (createdParticipant.eventId.Equals(mainWindow.selectedEvent.id.ToString()))
                                    {
                                        mainWindow.selectedEventParticipants.Add(createdParticipant);
                                    }
                                    mainWindow.UpdateCheckedInAndRegistered(mainWindow.selectedEventParticipants, mainWindow.selectedEvent, false);
                                    int filteredAmount = Int32.Parse(mainWindow.Label_FilteredAmount.Text) + 1;
                                    mainWindow.Label_FilteredAmount.Text = filteredAmount.ToString();
                                }
                                mainWindow.addParticipantTableRow();
                                mainWindow.addParticipantToParticipantTableRow(
                                    createdParticipant, mainWindow.Table_ParticipantsData1.RowCount - 1, events);
                            }
                        }
                    }
                    this.Dispose();
                }
                else
                {
                    messageBoxHelper.showWarning(this, "Event creation was unsuccesfull, because of internet connection" +
                        "or database write request number exceeded", "Warning");
                }
            }
            Button_Register.Enabled = true;
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
        private void ParticipationFormatSelectedIndexChanged(Object sender, EventArgs e)
        {
            if (this.ComboBox_ParticipationFormat.SelectedIndex == this.ComboBox_ParticipationFormat.Items.Count - 2 && !addNewParticipantFormatSelected)
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
            ComboBox_ParticipationFormat.Items.Add(deleteParticipationFormat);
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

        private void ComboBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? selectedEventIndex = ComboBox_Events.SelectedIndex;
            EventDaysShowHide(selectedEventIndex);
        }
        public void EventDaysShowHide(int? selectedEventIndex)
        {
            eventDayShowHide(Label_ParticipateDay1, ComboBox_ParticipateDay1, 1, selectedEventIndex);
            eventDayShowHide(Label_ParticipateDay2, ComboBox_ParticipateDay2, 2, selectedEventIndex);
            eventDayShowHide(Label_ParticipateDay3, ComboBox_ParticipateDay3, 3, selectedEventIndex);
            eventDayShowHide(Label_ParticipateDay4, ComboBox_ParticipateDay4, 4, selectedEventIndex);
        }
        public void eventDayShowHide(MetroLabel eventDayLabel, MetroComboBox eventDayComboBox, int dayValueToCompare,
            int? selectedEventIndex)
        {
            if (selectedEventIndex != null)
            {
                Event eventEntity = events[selectedEventIndex ?? (default)];
                if (eventEntity.eventLengthDays >= dayValueToCompare)
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
            else
            {
                eventDayLabel.Hide();
                eventDayComboBox.Hide();
            }
        }

        private void DateTime_PaymentDate_ValueChanged(object sender, EventArgs e)
        {
            paymentDateValueChanged = true;
            paymentValueDate = DateTime_PaymentDate.Value;
        }

        private void PictureBox_AddAdditionalPhoneNUmber_Click(object sender, EventArgs e)
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
            if(ComboBox_PaymentStatus.SelectedIndex == 2)
            {
                TextBox_PaymentAmount.Hide();
                Label_PaymentAmount.Hide();
            }
            else
            {
                TextBox_PaymentAmount.Show();
                Label_PaymentAmount.Show();
            }
            if(ComboBox_PaymentStatus.SelectedIndex == 1)
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
