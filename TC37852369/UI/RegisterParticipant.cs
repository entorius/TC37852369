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
using TC37852369.Services;
using TC37852369.UI.helpers;

public enum CompanyTypes
{
    Industry,
    Service,
    Media,
    Government
}
//Delete when all participation formats will be added to the database
public enum ParticipationFormats
{
     Delegate,
     Exhibitor,
     Speaker,
     SpeakerModerator,
     SpeakerChairman,
     StrategicPartner,
     MediaPartner,
     SponsorBronze,
     SponsorSilver,
     SponsorGold,
     SponsorPlatinum,
     SponsorEvening,
     SponsorIdentity,
     SponsorLunch,
     SponsorCoffee
}

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
        
        public List<string> participationFormats = new List<string>();
        string addNewParticipationFormat = "+ Add new participation format";
        bool addNewParticipantFormatSelected = false;
        public RegisterParticipant(MainWindow window, List<Event> events)
        {
            mainWindow = window;
            this.events = events;
            this.FormClosed += CloseHandler;

            InitializeComponent();
            EventDaysShowHide(null); ;
            foreach (Event eventEntity in events)
            {
                ComboBox_Events.Items.Add(eventEntity.eventName);
            }
            foreach (CompanyTypes companyType in (CompanyTypes[])Enum.GetValues(typeof(CompanyTypes)))
            {
                ComboBox_CompanyType.Items.Add(companyType);
            }

            foreach (ParticipationFormats participationFormat in (ParticipationFormats[])Enum.GetValues(typeof(ParticipationFormats)))
            {
                participationFormats.Add(participationFormat.ToString());
            }
            foreach(string participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat);
            }

            //Set payment status values to comboBox and select first item
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus);
            }
            ComboBox_PaymentStatus.SelectedIndex = 0;

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
            ComboBox_Materials.SelectedIndex = 1;
            ComboBox_ParticipateEveningEvent.SelectedIndex = 1;
            ComboBox_ParticipateDay1.SelectedIndex = 0;
            ComboBox_ParticipateDay2.SelectedIndex = 1;
            ComboBox_ParticipateDay3.SelectedIndex = 1;
            ComboBox_ParticipateDay4.SelectedIndex = 1;

            ComboBox_PaymentStatus.SelectedIndex = 0;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.SelectedIndexChanged += ParticipationFormatSelectedIndexChanged;

        }

        private async void Button_Register_Click(object sender, EventArgs e)
        {
            bool eventChoosen = ComboBox_Events.SelectedItem == null ? false : true;
            int partcipantInformationManditoryFieldsFilled =
                participantServices.isPartcipantInformationManditoryFieldsCorrect(
                    TextBox_FirstName.Text, TextBox_LastName.Text, TextBox_Email.Text
                    );
            if (partcipantInformationManditoryFieldsFilled > 0)
            {
                if(partcipantInformationManditoryFieldsFilled == 1)
                {
                    messageBoxHelper.showWarning(this, 
                        "First Name field text is too short (not correct name entered " +
                        "(is shorter than 3 characters) or name has not been entered)","Warning");
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
            }
            else
            {
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
                ComboBox_ParticipateDay4.Text.Equals("Yes") ? true : false
                );
                if (createdParticipant != null)
                {
                    mainWindow.Enabled = true;
                    mainWindow.participants.Add(createdParticipant);
                    mainWindow.addParticipantTableRow();
                    mainWindow.addParticipantToParticipantTableRow(
                        createdParticipant, mainWindow.Table_ParticipantData.RowCount - 1);
                    this.Dispose();
                }
                else
                {
                    messageBoxHelper.showWarning(this,"Event creation was unsuccesfull, because of internet connection" +
                        "or database write request number exceeded", "Warning");
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
        private void ParticipationFormatSelectedIndexChanged(Object sender, EventArgs e)
        {
            if(this.ComboBox_ParticipationFormat.SelectedIndex == this.ComboBox_ParticipationFormat.Items.Count - 1 && !addNewParticipantFormatSelected)
            {
                Console.WriteLine("Yahooo");
                this.ComboBox_ParticipationFormat.SelectedIndex = -1;
                this.Enabled = false;
                MetroForm registerParticipationFormat = new RegisterParticipationString(this);
                registerParticipationFormat.Show();
                registerParticipationFormat.Disposed += AddParticipationFormats;
                addNewParticipantFormatSelected = true;
            }
        }
        private void AddParticipationFormats(Object sender,EventArgs e)
        {
            ComboBox_ParticipationFormat.Items.Clear();
            foreach(string participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat);
            }
            ComboBox_ParticipationFormat.SelectedIndex = ComboBox_ParticipationFormat.Items.Count - 1;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
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
        public void eventDayShowHide(MetroLabel eventDayLabel,MetroComboBox eventDayComboBox, int dayValueToCompare, 
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
    }
}
