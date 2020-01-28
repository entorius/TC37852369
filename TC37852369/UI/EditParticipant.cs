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
        int rowNumber;

        public List<string> participationFormats = new List<string>();
        string addNewParticipationFormat = "+ Add new participation format";
        bool addNewParticipantFormatSelected = false;
        public EditParticipant(MainWindow window, Participant participant,List<Event> events,int rowNumber)
        {
            this.rowNumber = rowNumber;
            this.participant = participant;
            participantEvent = events.FindLast(
                 delegate (Event e)
                 {
                     return e.id == long.Parse(participant.eventId);
                 }
                );
            mainWindow = window;
            InitializeComponent();
            this.FormClosed += CloseHandler;
            
            EventDaysShowHide(participantEvent.eventLengthDays);
            /*foreach (Event eventEntity in events)
            {
                ComboBox_Events.Items.Add(eventEntity.eventName);
            }*/
            Label_EventName.Text = participantEvent.eventName;
            foreach (CompanyTypes companyType in (CompanyTypes[])Enum.GetValues(typeof(CompanyTypes)))
            {
                ComboBox_CompanyType.Items.Add(companyType);
            }

            foreach (ParticipationFormats participationFormat in (ParticipationFormats[])Enum.GetValues(typeof(ParticipationFormats)))
            {
                participationFormats.Add(participationFormat.ToString());
            }
            foreach (string participationFormat in participationFormats)
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

            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.SelectedIndexChanged += ParticipationFormatSelectedIndexChanged;


            setFieldsData();
        }

       

        

        protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            //bool eventChoosen = ComboBox_Events.SelectedItem == null ? false : true;
            int partcipantInformationManditoryFieldsFilled =
                participantServices.isPartcipantInformationManditoryFieldsCorrect(
                    TextBox_FirstName.Text, TextBox_LastName.Text, TextBox_Email.Text
                    );
            if (partcipantInformationManditoryFieldsFilled > 0)
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
            }
            else
            {
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
                participant.checkedInDay4
                );
                await participantServices.editParticipant(createdParticipant);
                if (createdParticipant != null)
                {
                    mainWindow.Enabled = true;
                    int participantRow = this.mainWindow.participants.FindLastIndex(delegate (Participant participant)
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
                    tableLayoutHelper.RemoveArbitraryRow(mainWindow.Table_ParticipantData, rowNumber);
                    mainWindow.participants.Remove(participant);
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
        }
        private void AddParticipationFormats(Object sender, EventArgs e)
        {
            ComboBox_ParticipationFormat.Items.Clear();
            foreach (string participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat);
            }
            ComboBox_ParticipationFormat.SelectedIndex = ComboBox_ParticipationFormat.Items.Count - 1;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
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
    }
}
