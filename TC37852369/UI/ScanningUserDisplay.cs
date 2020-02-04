using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TC37852369.DomainEntities;
using TC37852369.Services;
using TC37852369.UI.helpers;

namespace TC37852369.UI
{
    public partial class ScanningUserDisplay : MetroForm
    {
        ParticipantServices participantServices = new ParticipantServices();
        List<Participant> participants;
        List<Event> events;
        string barcode;
        Participant selectedParticipant = null;
        Event selectedEvent = null;
        MainWindow mainWindow;
        ScanningWindow scanningWindow;
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        
        public ScanningUserDisplay(ScanningWindow scanningWindow,MainWindow mainWindow, List<Participant> participants, List<Event> events, string barcode)
        {
            InitializeComponent();
            this.participants = participants;
            this.events = events;
            this.barcode = barcode;
            this.mainWindow = mainWindow;
            this.scanningWindow = scanningWindow;
            this.FormClosed += ClosedHandler;
            loadWindowData();
        }

        private void ClosedHandler(object sender, FormClosedEventArgs e)
        {
            mainWindow.Enabled = true;
            scanningWindow.Dispose();
        }

        private void loadWindowData()
        {
            selectedParticipant = participants.Find(p => p.ticketBarcode.Equals(barcode));
            if (selectedParticipant != null)
            {
                selectedEvent = events.Find(e => e.id.ToString().Equals(selectedParticipant.eventId));

                int eventDaysNumber = selectedEvent.eventLengthDays;
                Label_EventName.Text = selectedEvent.eventName;
                Label_FirstName.Text = selectedParticipant.firstName;
                Label_LastName.Text = selectedParticipant.lastName;
                Label_PaymentStatus.Text = selectedParticipant.paymentStatus;

                Label_CheckInDay1.Text = selectedParticipant.checkedInDay1 ? "Yes" : "No";
                Label_CheckInDay2.Text = selectedParticipant.checkedInDay2 ? "Yes" : "No";
                Label_CheckInDay3.Text = selectedParticipant.checkedInDay3 ? "Yes" : "No";
                Label_CheckInDay4.Text = selectedParticipant.checkedInDay4 ? "Yes" : "No";
                Label_RegisteredInDay1.Text = selectedParticipant.participateInDay1 ? "Yes" : "No";
                Label_RegisteredInDay2.Text = selectedParticipant.participateInDay2 ? "Yes" : "No";
                Label_RegisteredInDay3.Text = selectedParticipant.participateInDay3 ? "Yes" : "No";
                Label_RegisteredInDay4.Text = selectedParticipant.participateInDay4 ? "Yes" : "No";

                ShowCheckedInRegisteredLabel(Label_CheckInDay1Name, Label_RegisteredInDay1Name,Label_CheckInDay1, Label_RegisteredInDay1, eventDaysNumber, 1);
                ShowCheckedInRegisteredLabel(Label_CheckInDay2Name, Label_RegisteredInDay2Name, Label_CheckInDay2, Label_RegisteredInDay2, eventDaysNumber, 2);
                ShowCheckedInRegisteredLabel(Label_CheckInDay3Name, Label_RegisteredInDay3Name, Label_CheckInDay3, Label_RegisteredInDay3, eventDaysNumber, 3);
                ShowCheckedInRegisteredLabel(Label_CheckInDay4Name, Label_RegisteredInDay4Name, Label_CheckInDay4, Label_RegisteredInDay4, eventDaysNumber, 4);
            }
            else
            {
                messageBoxHelper.showWarning(this, "Scanned user does not exist in the database!", "Warning");
            }

        }
        public void ShowCheckedInRegisteredLabel(Label checkedInLabelName, Label registeredLabelName, Label checkedInLabel, Label registeredLabel, int eventDaysNumber, 
            int eventDay)
        {
            if(eventDaysNumber >= eventDay){
                checkedInLabel.Show();
                registeredLabel.Show();
                checkedInLabelName.Show();
                registeredLabelName.Show();
            }
            else
            {
                checkedInLabel.Hide();
                registeredLabel.Hide();
                checkedInLabelName.Hide();
                registeredLabelName.Hide();
            }
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            scanningWindow.Dispose();
            mainWindow.BringToFront();
            this.Dispose();
        }

        private async void Button_CheckIn_Click(object sender, EventArgs e)
        {
            DateTime today = getToday();
            bool successful = false;
            if (selectedEvent != null)
            {
                if (today == selectedEvent.date_From && selectedParticipant.participateInDay1)
                {
                    selectedParticipant.checkedInDay1 = true;
                    successful = true;
                    
                }
                else if (today == selectedEvent.date_From.AddDays(1) && selectedEvent.eventLengthDays >= 2
                    && selectedParticipant.participateInDay2)
                {
                    selectedParticipant.checkedInDay2 = true;
                    successful = true;
                }
                else if (today == selectedEvent.date_From.AddDays(2) && selectedEvent.eventLengthDays >= 3
                    && selectedParticipant.participateInDay3)
                {
                    selectedParticipant.checkedInDay3 = true;
                    successful = true;
                }
                else if (today == selectedEvent.date_From.AddDays(3) && selectedEvent.eventLengthDays >= 4
                    && selectedParticipant.participateInDay4)
                {
                    selectedParticipant.checkedInDay4 = true;
                    successful = true;
                }
                else
                {
                    messageBoxHelper.showWarning(this, 
                        "Check in unsuccessful. Possible problems:" +
                        "1) Event is ended\n" +
                        "2) Event has not started yet\n" +
                        "3) Participant is not participanting in this event day",
                        "Warning");
                }
                if (successful)
                {
                    Participant resultParticipant = await participantServices.editParticipant(selectedParticipant);
                    if(resultParticipant != null)
                    {
                        int filteredParticipantsId = mainWindow.filteredParticipants.FindIndex(p => p.participantId.Equals(resultParticipant.participantId));
                        int allParticipantsId = mainWindow.allParticipants.FindIndex(p => p.participantId.Equals(resultParticipant.participantId));

                        mainWindow.filteredParticipants[filteredParticipantsId] = resultParticipant;
                        mainWindow.allParticipants[allParticipantsId] = resultParticipant;

                        mainWindow.editParticipantTableRow(resultParticipant, filteredParticipantsId);
                        mainWindow.Enabled = true;
                        scanningWindow.Dispose();
                        this.Dispose();
                    }
                    else
                    {
                        messageBoxHelper.showWarning(this,
                        "Participant saving was unsuccessful. Check your internet connection. If problem continues " +
                        "contact support (might be problems with database or server)",
                        "Warning");
                    }
                }
            }
        }
        public DateTime getToday()
        {
            DateTime today = DateTime.Now;
            return new DateTime(
                today.Year,
                today.Month,
                today.Day,
                0,
                0,
                0,
                0);
        }
    }
}
