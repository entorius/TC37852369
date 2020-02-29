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
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class MainWindow : MetroForm
    {
        Login login;
        //events variables
        public EventServices eventService = new EventServices();
        public List<Event> events = new List<Event>();
        int lastEventsCount = 0;
        public List<Event> filteredEvents = new List<Event>();
        //registration variables
        public Event selectedEvent;
        public ParticipantServices participantService = new ParticipantServices();
        public EventServices eventServices = new EventServices();
        public List<Participant> allParticipants = new List<Participant>();
        public List<Participant> selectedEventParticipants = new List<Participant>();
        public List<Participant> filteredParticipants = new List<Participant>();
        public List<bool> participantTableColumnShow = new List<bool>();
        private CompanyDataServices companyDataServices = new CompanyDataServices();
        private MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        public CompanyData companyData;
        public List<EmailTemplate> mailTemplates;
        public List<List<ToolTipHelper>> participantsToolTipHelpers = new List<List<ToolTipHelper>>();
        private MailTemplateServices mailTemplateService = new MailTemplateServices();
        int regiteredParticipantsTodayNumber = 0;
        int checkedInTodayParticipantsNumber = 0;
        WindowLoading windowLoading;
        public FilterWindowData filterWindowData;
        public int lastEventFilteringYear = 0;
        public int lastEventFilteringMonth = 0;
        public int lastEventFilteringDay = 0;
        public string lastEventFilteringEventName = "";
        public MainWindow(Login login)
        {
           
            
            this.login = login;
            this.FormClosed += ClosedHandler;
            InitializeComponent();
            Table_ParticipantsData1.BringToFront();
            windowLoading = new WindowLoading(this);
            windowLoading.Show();
            loadData();
            this.Show();
            this.BringToFront();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            Label_EventDayNumber.Hide();
        }
        public async void loadData()
        {

            events = await eventService.getAllEvents();
            filteredEvents = events;
            //Participant participant1 = await participantService.getParticipant("253");
            //participants.Add(participant1);
            allParticipants = await participantService.getAllParticipants();
            companyData = await companyDataServices.GetCompanyData();
            mailTemplates = await mailTemplateService.getAllMailTemplates();
            if (events.Count > 0)
            {
                Table_EventsData.SuspendLayout();
                UpdateEventsTable(filteredEvents);
                Table_EventsData.ResumeLayout();
                foreach (Event eventEntity in events)
                {
                    ComboBox_Events.Items.Add(eventEntity.eventName);
                }
            }
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus.ToString());
            }
            ComboBox_PaymentStatus.SelectedIndex = -1;

            Label_Registered.Hide();
            Label_Filtered.Hide();
            Label_CheckedIn.Hide();
            Label_RegisteredAmount.Hide();
            Label_FilteredAmount.Hide();
            Label_CheckedInAmount.Hide();

            Label_PaidStatus.Hide();
            Label_PaidDueAmount.Hide();

            //Events counts amount labels
            Label_EventsNumberAmount.Text = events.Count.ToString();
            Label_FilteredEventsAmount.Text = filteredEvents.Count.ToString();

            metroTabControl2.Selecting += TabChanged;

            updateEventExportItems();
            windowLoading.Dispose();
            this.Enabled = true;
            for (int i = 0; i <= 15; i++)
            {
                participantTableColumnShow.Add(true);
            }
            //UpdateParticipantsTable();
            filterWindowData = new FilterWindowData("", "", "", "", "", "", "","","","","","","", false, "", false, false, "", false, false, false, false,
            false, false,false, false, false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,
            false,false);
        }

        private void TabChanged(object sender, EventArgs e)
        {
            if (events.Count > lastEventsCount)
            {
                updateEventExportItems();
            }
        }
        private void updateEventExportItems()
        {
            ComboBox_EventExport.Items.Clear();
            foreach (Event ev in events)
            {
                ComboBox_EventExport.Items.Add(ev.eventName);
            }
            lastEventsCount = events.Count;
        }

        //Main Window Events tab actions
        public void UpdateEventsTable(List<Event> eventsToAdd)
        {

            Table_EventsData.SuspendLayout();
            emptyTable(Table_EventsData);
            
            Table_EventsData.RowCount = eventsToAdd.Count;
            for (int i = 0; i < eventsToAdd.Count - 1; i++)
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
            for (int i = 0; i < eventsToAdd.Count; i++)
            {
                addEventToEventTableRow(eventsToAdd[i], i);
            }
            Table_EventsData.ResumeLayout();
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
            label_EventName.Width = 250;
            Label label_EventDate = new Label();
            label_EventDate.Text = eventEntity.date_From.ToString("dd/MM/yyyy");
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
            button_Edit.BackColor = ColorTranslator.FromHtml("#48b1bf");
            button_Edit.FontSize = MetroFramework.MetroButtonSize.Medium;
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

        public void UpdateParticipantsTable(List<Participant> ParticipantsToAdd)
        {

            Table_ParticipantsData1.SuspendLayout();
            Table_ParticipantsData1.RowCount = ParticipantsToAdd.Count;
            for (int i = 0; i < ParticipantsToAdd.Count - 1; i++)
            {
                Table_ParticipantsData1.RowStyles.Add(new RowStyle(SizeType.Absolute));
            }
            TableLayoutRowStyleCollection styles =
            Table_ParticipantsData1.RowStyles;
            int j = 0;
            foreach (RowStyle style in styles)
            {
                // Set the row height to 20 pixels.
                style.SizeType = SizeType.Absolute;
                style.Height = 40;
                j = j + 1;

            }
            table_ParticipantsHeader.SuspendLayout();
            for (int i = 0; i < ParticipantsToAdd.Count; i++)
            {
                addParticipantToParticipantTableRow(ParticipantsToAdd[i], i, events);
            }
            table_ParticipantsHeader.ResumeLayout();
            Table_ParticipantsData1.ResumeLayout();

        }
        public void addParticipantToParticipantTableRow(Participant participant, int rowNumber,
            List<Event> events)
        {
            DateTime todayDate = DateHelper.getToday();
            int eventDaysParticipating = participantService.countInHowManyDaysParticipating(
                participant);
            Event participantEvent = events.Find(delegate (Event eventValue)
            {
                return eventValue.id == long.Parse(participant.eventId);
            });

            double paymentAmount = 0;
            if (participant.paymentStatus.Equals("Free"))
            {
                paymentAmount = 0;
            }
            else
            {
                paymentAmount = participant.paymentAmount;
            }

            emptyTableRowsAndColumns(table_ParticipantsHeader);

            List<ToolTipHelper> toolTipHelpers = new List<ToolTipHelper>();

            Label label_FirstName = new Label();
            label_FirstName.Text = participant.firstName;
            label_FirstName.Width = 135;
            ToolTipHelper toolTipHelperFirstName = new ToolTipHelper();
            toolTipHelperFirstName.getLabelToolTip(label_FirstName, participant.firstName);
            toolTipHelpers.Add(toolTipHelperFirstName);

            Label label_LastName = new Label();
            label_LastName.Text = participant.lastName;
            label_LastName.Width = 135;
            ToolTipHelper toolTipHelperLastName = new ToolTipHelper();
            toolTipHelperLastName.getLabelToolTip(label_LastName, participant.lastName);
            toolTipHelpers.Add(toolTipHelperLastName);

            Label label_Country = new Label();
            label_Country.Text = participant.country;
            label_Country.Width = 135;
            ToolTipHelper toolTipHelperCoutry = new ToolTipHelper();
            toolTipHelperCoutry.getLabelToolTip(label_Country, participant.country);
            toolTipHelpers.Add(toolTipHelperCoutry);

            Label label_JobTitle = new Label();
            label_JobTitle.Text = participant.jobTitle;
            label_JobTitle.Width = 135;
            ToolTipHelper toolTipHelperJobTitle = new ToolTipHelper();
            toolTipHelperJobTitle.getLabelToolTip(label_JobTitle, participant.jobTitle);
            toolTipHelpers.Add(toolTipHelperJobTitle);

            Label label_CompanyType = new Label();
            label_CompanyType.Text = participant.companyType;
            label_CompanyType.Width = 135;

            Label label_CompanyName = new Label();
            label_CompanyName.Text = participant.companyName;
            label_CompanyName.Width = 135;
            ToolTipHelper toolTipHelperCompanyName = new ToolTipHelper();
            toolTipHelperCompanyName.getLabelToolTip(label_CompanyName, participant.companyName);
            toolTipHelpers.Add(toolTipHelperCompanyName);

            Label label_ParticipationFormat = new Label();
            label_ParticipationFormat.Text = participant.participationFormat;
            label_ParticipationFormat.Width = 146;
            ToolTipHelper toolTipHelperParticipationFormat = new ToolTipHelper();
            toolTipHelperParticipationFormat.getLabelToolTip(label_ParticipationFormat, participant.participationFormat);
            toolTipHelpers.Add(toolTipHelperParticipationFormat);

            Label label_PaymentStatus = new Label();
            label_PaymentStatus.Text = participant.paymentStatus;
            label_PaymentStatus.Width = 100;

            Label label_PaymentAmount = new Label();
            label_PaymentAmount.Text = paymentAmount.ToString();
            label_PaymentAmount.Width = 100;

            Label label_RegistrationDate = new Label();
            label_RegistrationDate.Text = participant.registrationDate.ToString("dd/MM/yyyy");
            label_RegistrationDate.Width = 100;

            Label label_PaymentDate = new Label();
            label_PaymentDate.Text = participant.paymentStatus.Equals("Paid") ? participant.paymentDate.ToString("dd/MM/yyyy"): "-";
            label_PaymentDate.Width = 100;

            Label label_TicketSent = new Label();
            label_TicketSent.Text = participant.ticketSent ? "yes" : "no";
            label_TicketSent.Width = 150;

            Label label_CheckedInDay = new Label();
            label_CheckedInDay.Text = "";
            label_CheckedInDay.Width = 100;

            Label label_RegisteredInDay = new Label();
            label_RegisteredInDay.Text = "";
            label_RegisteredInDay.Width = 100;

            string headerCheckedInDay = "";
            string headerRegisteredInDay = "";

            participantsToolTipHelpers.Add(toolTipHelpers);

            if (participantEvent.date_From <= todayDate && participantEvent.date_From.AddDays(participantEvent.eventLengthDays - 1) >= todayDate)
            {
                if (participantEvent.date_From.Equals(todayDate))
                {
                    label_CheckedInDay.Text = participant.checkedInDay1 ? "yes" : "no";
                    headerCheckedInDay = "Checked in day 1";
                    label_RegisteredInDay.Text = participant.participateInDay1 ? "yes" : "no";
                    headerRegisteredInDay = "Registered in day 1";
                }
                else if (participantEvent.date_From.AddDays(1).Equals(todayDate))
                {
                    label_CheckedInDay.Text = participant.checkedInDay2 ? "yes" : "no";
                    headerCheckedInDay = "Checked in day 2";
                    label_RegisteredInDay.Text = participant.participateInDay2 ? "yes" : "no";
                    headerRegisteredInDay = "Registered in day 2";
                }
                else if (participantEvent.date_From.AddDays(2).Equals(todayDate))
                {
                    label_CheckedInDay.Text = participant.checkedInDay3 ? "yes" : "no";
                    headerCheckedInDay = "Checked in day 3";
                    label_RegisteredInDay.Text = participant.participateInDay3 ? "yes" : "no";
                    headerRegisteredInDay = "Registered in day 3";
                }
                else if (participantEvent.date_From.AddDays(3).Equals(todayDate))
                {
                    label_CheckedInDay.Text = participant.checkedInDay4 ? "yes" : "no";
                    headerCheckedInDay = "Checked in day 4";
                    label_RegisteredInDay.Text = participant.participateInDay4 ? "yes" : "no";
                    headerRegisteredInDay = "Registered in day 4";
                }
            }
            Label label_FirstNameHeader = formatLabel(9, 150,45, "First Name", Color.White);
            Label label_LastNameHeader = formatLabel(9, 150, 45, "Last Name", Color.White);
            Label label_CountryHeader = formatLabel(9, 150, 45, "Country", Color.White);
            Label label_JobTitleHeader = formatLabel(9, 150, 45, "Job title", Color.White);
            Label label_CompanyTypeHeader = formatLabel(9, 150, 45, "Company type", Color.White);
            Label label_CompanyNameHeader = formatLabel(9, 150, 45, "Company name", Color.White);
            Label label_ParticipationFormatHeader = formatLabel(9, 160, 45, "Participation format", Color.White);
            Label label_PaymentStatusHeader = formatLabel(9, 100, 45, "Payment status", Color.White);
            Label label_PaymentAmountHeader = formatLabel(9, 100, 45, "Payment amount", Color.White);
            Label label_RegistrationDateHeader = formatLabel(9, 100, 45, "Registration Date", Color.White);
            Label label_PaymentDateHeader = formatLabel(9, 100, 45, "Payment Date", Color.White);
            Label label_TicketSentHeader = formatLabel(9, 150, 45, "Ticket sent", Color.White);
            Label label_EditHeader = formatLabel(9, 150, 45, "Edit", Color.White);
            Label label_CheckInHeader = formatLabel(9, 100, 45, "CheckIn", Color.White);
            Label label_CheckedInDayHeader = formatLabel(9, 100, 45, headerCheckedInDay, Color.White);
            Label label_RegisteredInDayHeader = formatLabel(9, 100, 45, headerRegisteredInDay, Color.White);

            Table_ParticipantsData1.ColumnCount = 13;
            table_ParticipantsHeader.ColumnCount = 13;

            table_ParticipantsHeader.Controls.Add(label_FirstNameHeader, 0, 0);
            table_ParticipantsHeader.Controls.Add(label_LastNameHeader, 1, 0);
            table_ParticipantsHeader.Controls.Add(label_CountryHeader, 2, 0);
            table_ParticipantsHeader.Controls.Add(label_JobTitleHeader, 3, 0);
            table_ParticipantsHeader.Controls.Add(label_CompanyTypeHeader, 4, 0);
            table_ParticipantsHeader.Controls.Add(label_CompanyNameHeader, 5, 0);
            table_ParticipantsHeader.Controls.Add(label_ParticipationFormatHeader, 6, 0);
            table_ParticipantsHeader.Controls.Add(label_PaymentStatusHeader, 7, 0);
            table_ParticipantsHeader.Controls.Add(label_PaymentAmountHeader, 8, 0);
            table_ParticipantsHeader.Controls.Add(label_RegistrationDateHeader, 9, 0);
            table_ParticipantsHeader.Controls.Add(label_PaymentDateHeader, 10, 0);
            table_ParticipantsHeader.Controls.Add(label_TicketSentHeader, 11, 0);
            table_ParticipantsHeader.Controls.Add(label_EditHeader, 12, 0);

            table_ParticipantsHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            if (headerCheckedInDay.Length > 0)
            {
                Table_ParticipantsData1.ColumnCount = 16;
                table_ParticipantsHeader.ColumnCount = 16;

                table_ParticipantsHeader.Controls.Add(label_RegisteredInDayHeader, 13, 0);
                table_ParticipantsHeader.Controls.Add(label_CheckedInDayHeader, 14, 0);
                table_ParticipantsHeader.Controls.Add(label_CheckInHeader, 15, 0);


            }
            MetroButton button_Edit = new MetroButton();
            button_Edit.Height = 40;
            button_Edit.Width = 80;
            button_Edit.Text = "Edit";
            button_Edit.Margin = new System.Windows.Forms.Padding(35, 0, 0, 0);
            button_Edit.BackColor = ColorTranslator.FromHtml("#48b1bf");
            button_Edit.ForeColor = Color.White;
            button_Edit.UseCustomBackColor = true;
            button_Edit.UseCustomForeColor = true;
            button_Edit.MouseClick += ButtonEditParticipantHandler;
            button_Edit.FontSize = MetroFramework.MetroButtonSize.Medium;

            MetroButton button_CheckIn = new MetroButton();
            button_CheckIn.Height = 40;
            button_CheckIn.Width = 80;
            button_CheckIn.Text = "Checkin";
            button_CheckIn.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            button_CheckIn.BackColor = ColorTranslator.FromHtml("#56ab2f");
            button_CheckIn.ForeColor = Color.White;
            button_CheckIn.UseCustomBackColor = true;
            button_CheckIn.UseCustomForeColor = true;
            button_CheckIn.MouseClick += ButtonCheckInParticipantHandler;
            button_CheckIn.FontSize = MetroFramework.MetroButtonSize.Medium;

            Table_ParticipantsData1.Controls.Add(label_FirstName, 0, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_LastName, 1, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_Country, 2, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_JobTitle, 3, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_CompanyType, 4, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_CompanyName, 5, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_ParticipationFormat, 6, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_PaymentStatus, 7, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_PaymentAmount, 8, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_RegistrationDate, 9, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_PaymentDate, 10, rowNumber);
            Table_ParticipantsData1.Controls.Add(label_TicketSent, 11, rowNumber);
            Table_ParticipantsData1.Controls.Add(button_Edit, 12, rowNumber);
            if (Table_ParticipantsData1.ColumnCount == 16)
            {
                Table_ParticipantsData1.Controls.Add(label_RegisteredInDay, 13, rowNumber);
                Table_ParticipantsData1.Controls.Add(label_CheckedInDay, 14, rowNumber);
                Table_ParticipantsData1.Controls.Add(button_CheckIn, 15, rowNumber);
            }

        }
        private void Button_RegisterParticipant_Click(object sender, EventArgs e)
        {
            RegisterParticipant cEvent = new RegisterParticipant(this, events);
            cEvent.BringToFront();
            this.Enabled = false;
            cEvent.Show();
            cEvent.BringToFront();
        }
        private void ButtonEditParticipantHandler(object sender, EventArgs e)
        {
            var cellPos = GetRowColIndex(
            Table_ParticipantsData1,
            Table_ParticipantsData1.PointToClient(Cursor.Position));
            EditParticipant editParticipant = new EditParticipant(this,
                filteredParticipants[cellPos.Value.Y], events, companyData, cellPos.Value.Y);
            editParticipant.Show();
            this.Enabled = false;
        }
        private async void ButtonCheckInParticipantHandler(object sender, EventArgs e)
        {
            DateTime todayDate = DateHelper.getToday();
            var cellPos = GetRowColIndex(
            Table_ParticipantsData1,
            Table_ParticipantsData1.PointToClient(Cursor.Position));
            Participant checkingInParticipant = filteredParticipants[cellPos.Value.Y];
            bool registeredInEventDay = false;

            if (selectedEvent.date_From <= todayDate && selectedEvent.date_From.AddDays(selectedEvent.eventLengthDays - 1) >= todayDate)
            {
                if (selectedEvent.date_From.Equals(todayDate) && checkingInParticipant.participateInDay1)
                {
                    registeredInEventDay = true;
                }
                else if (selectedEvent.date_From.AddDays(1).Equals(todayDate) && checkingInParticipant.participateInDay2)
                {
                    registeredInEventDay = true;
                }
                else if (selectedEvent.date_From.AddDays(2).Equals(todayDate) && checkingInParticipant.participateInDay3)
                {
                    registeredInEventDay = true;
                }
                else if (selectedEvent.date_From.AddDays(3).Equals(todayDate) && checkingInParticipant.participateInDay4)
                {
                    registeredInEventDay = true;
                }
            }
            if (registeredInEventDay)
            {
                DialogResult dialogResult = messageBoxHelper.showYesNo(this, "Are you sure you want to check in " +
                    checkingInParticipant.firstName + " " + checkingInParticipant.lastName + "?", "Question");

                if (dialogResult == DialogResult.Yes)
                {
                    DateTime today = DateHelper.getToday();
                    if (today.Equals(selectedEvent.date_From))
                    {
                        checkingInParticipant.checkedInDay1 = true;
                    }
                    else if (today.Equals(selectedEvent.date_From.AddDays(1)))
                    {
                        checkingInParticipant.checkedInDay2 = true;
                    }
                    else if (today.Equals(selectedEvent.date_From.AddDays(2)))
                    {
                        checkingInParticipant.checkedInDay3 = true;
                    }
                    else if (today.Equals(selectedEvent.date_From.AddDays(3)))
                    {
                        checkingInParticipant.checkedInDay4 = true;
                    }

                    Participant addedParticipant = await participantService.editParticipant(checkingInParticipant);
                    if (addedParticipant != null)
                    {
                        filteredParticipants[cellPos.Value.Y] = checkingInParticipant;
                        int allParticipantsIndex = allParticipants.FindIndex(p => p.participantId.Equals(checkingInParticipant.participantId));
                        allParticipants[allParticipantsIndex] = checkingInParticipant;
                        int selectedEventParticipantId = selectedEventParticipants.FindIndex(p => p.participantId.Equals(checkingInParticipant.participantId));
                        selectedEventParticipants[selectedEventParticipantId] = checkingInParticipant;
                        editParticipantTableRow(checkingInParticipant, cellPos.Value.Y);
                        UpdateCheckedInAndRegistered(selectedEventParticipants, selectedEvent, false);
                    }
                    else
                    {
                        messageBoxHelper.showWarning(this, "Check in unsuccessful:\n" +
                            "1) Bad internet connection\n" +
                            "2) Database problems (if problem continues contact support)", "Warning");
                    }
                }
            }
            else
            {
                messageBoxHelper.showWarning(this, "Participant is not registered to this event day", "Warning");
            }
        }
        public void addParticipantTableRow()
        {
            this.addTableRow(Table_ParticipantsData1);
        }

        public void editParticipantTableRow(Participant participant, int rowNumber)
        {
            DateTime todayDate = DateHelper.getToday();
            int eventDaysParticipating = participantService.countInHowManyDaysParticipating(
                participant);
            Event participantEvent = events.Find(delegate (Event eventValue)
            {
                return eventValue.id == long.Parse(participant.eventId);
            });

            double paymentAmount = 0;
            if (participant.paymentAmount >= 0)
            {
                paymentAmount = participant.paymentAmount;
            }
            else {
                paymentAmount = 0;
            }
            Table_ParticipantsData1.GetControlFromPosition(0, rowNumber).Text =
                participant.firstName;
            participantsToolTipHelpers[rowNumber][0].labelToolTip
                .SetToolTip(Table_ParticipantsData1.GetControlFromPosition(0, rowNumber), participant.firstName);

            Table_ParticipantsData1.GetControlFromPosition(1, rowNumber).Text =
                participant.lastName;
            participantsToolTipHelpers[rowNumber][1].labelToolTip
                .SetToolTip(Table_ParticipantsData1.GetControlFromPosition(1, rowNumber), participant.lastName);

            Table_ParticipantsData1.GetControlFromPosition(2, rowNumber).Text =
                participant.jobTitle;
            participantsToolTipHelpers[rowNumber][2].labelToolTip
                .SetToolTip(Table_ParticipantsData1.GetControlFromPosition(2, rowNumber), participant.jobTitle);

            Table_ParticipantsData1.GetControlFromPosition(3, rowNumber).Text =
                participant.companyType;

            Table_ParticipantsData1.GetControlFromPosition(4, rowNumber).Text =
                participant.companyName;
            participantsToolTipHelpers[rowNumber][3].labelToolTip
                .SetToolTip(Table_ParticipantsData1.GetControlFromPosition(4, rowNumber), participant.companyName);

            Table_ParticipantsData1.GetControlFromPosition(5, rowNumber).Text =
                participant.participationFormat;
            participantsToolTipHelpers[rowNumber][4].labelToolTip
                .SetToolTip(Table_ParticipantsData1.GetControlFromPosition(5, rowNumber), participant.participationFormat);

            Table_ParticipantsData1.GetControlFromPosition(6, rowNumber).Text =
                participant.paymentStatus;

            Table_ParticipantsData1.GetControlFromPosition(7, rowNumber).Text =
                paymentAmount.ToString();

            Table_ParticipantsData1.GetControlFromPosition(8, rowNumber).Text =
                participant.registrationDate.ToString("dd/MM/yyyy");

            Table_ParticipantsData1.GetControlFromPosition(9, rowNumber).Text =
                participant.paymentStatus.Equals("Paid") ? participant.paymentDate.ToString("dd/MM/yyyy") : "-";

            Table_ParticipantsData1.GetControlFromPosition(10, rowNumber).Text =
                participant.ticketSent ? "yes" : "no";


            if (Table_ParticipantsData1.ColumnCount == 15)
            {
                if (participantEvent.date_From.Equals(todayDate))
                {
                    Table_ParticipantsData1.GetControlFromPosition(12, rowNumber).Text =
                   participant.participateInDay1 ? "yes" : "no";

                    Table_ParticipantsData1.GetControlFromPosition(13, rowNumber).Text =
                    participant.checkedInDay1 ? "yes" : "no";
                }
                else if (participantEvent.date_From.AddDays(1).Equals(todayDate))
                {
                    Table_ParticipantsData1.GetControlFromPosition(12, rowNumber).Text =
                   participant.participateInDay2 ? "yes" : "no";

                    Table_ParticipantsData1.GetControlFromPosition(13, rowNumber).Text =
                    participant.checkedInDay2 ? "yes" : "no";
                }
                else if (participantEvent.date_From.AddDays(2).Equals(todayDate))
                {
                    Table_ParticipantsData1.GetControlFromPosition(12, rowNumber).Text =
                   participant.participateInDay3 ? "yes" : "no";

                    Table_ParticipantsData1.GetControlFromPosition(13, rowNumber).Text =
                    participant.checkedInDay3 ? "yes" : "no";
                }
                else if (participantEvent.date_From.AddDays(3).Equals(todayDate))
                {
                    Table_ParticipantsData1.GetControlFromPosition(12, rowNumber).Text =
                   participant.participateInDay4 ? "yes" : "no";

                    Table_ParticipantsData1.GetControlFromPosition(13, rowNumber).Text =
                    participant.checkedInDay4 ? "yes" : "no";
                }
            }

            filteredParticipants[rowNumber] = participant;

            int idToReplace = allParticipants.FindLastIndex(p => p.participantId.Equals(participant.participantId));
            allParticipants[idToReplace] = participant;
            int selectedEventIdToReplace = selectedEventParticipants.FindLastIndex(p => p.participantId.Equals(participant.participantId));
            selectedEventParticipants[selectedEventIdToReplace] = participant;
            double paymentAmount1 = participantService.countParticipantsPaymentAmount(filteredParticipants);
            Label_PaidDueAmount.Text = paymentAmount1.ToString();
        }

        protected void ClosedHandler(object sender, EventArgs e)
        {
            login.Show();
        }

        private void Button_GenerateMail_Click(object sender, EventArgs e)
        {
            GenerateSend generateSend = new GenerateSend(this, events, allParticipants, companyData,
                mailTemplates);
            generateSend.Show();
            this.Enabled = false;
        }

        private void Button_AddUser_Click(object sender, EventArgs e)
        {
            CreateUser user = new CreateUser(this);
            user.Show();
            user.BringToFront();
            this.Enabled = false;
            user.BringToFront();
        }

        private void Button_GenerateTicket_Click(object sender, EventArgs e)
        {
            Event eventEntity = events[0];

            if (allParticipants.Count > 0)
            {
                eventEntity = events.Find(ev =>
                ev.id.ToString().Equals(allParticipants[0].eventId));

            }

            GenerateTicket ticket = new GenerateTicket(this, companyData, events);
            ticket.Show();
            ticket.BringToFront();
            this.Enabled = false;
            ticket.BringToFront();
        }

        private void Button_EditEmail_Click(object sender, EventArgs e)
        {
            EditEmailTemplate email = new EditEmailTemplate(this);
            email.Show();
            email.Hide();
            
            this.Enabled = false;
            email.Show();
            email.BringToFront();
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            if (ComboBox_EventExport.SelectedIndex >= 0)
            {
                DialogResult dialogResult = FolderBrowserDialog_Events.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    List<Participant> eParticipants = participantService.
                        filterEventParticipants(allParticipants, events[ComboBox_EventExport.SelectedIndex]);
                    GenerateExcel.ExportAllEventInfo(events[ComboBox_EventExport.SelectedIndex], 
                        eParticipants, FolderBrowserDialog_Events.SelectedPath, 
                        events[ComboBox_EventExport.SelectedIndex].eventName + "_Export_All");
                }
            }
            else
            {
                messageBoxHelper.showWarning(this, "You have not choosen event", "Warning");
            }
        }

        private void Button_ChangeInformation_Click(object sender, EventArgs e)
        {
            AddChangeCompanyInformation changeInformationWindow =
                new AddChangeCompanyInformation(this);
            changeInformationWindow.Show();
            changeInformationWindow.BringToFront();
            this.Enabled = false;
            changeInformationWindow.BringToFront();
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

        private void Button_CheckIn_Click(object sender, EventArgs e)
        {
            ScanningWindow scanningWindow = new ScanningWindow(this, events, allParticipants);
            scanningWindow.Show();
            scanningWindow.BringToFront();
            this.Enabled = false;
        }

        private async void Button_UpdateParticipantsInformation_Click(object sender, EventArgs e)
        {
            if (this.selectedEvent != null)
            {
                allParticipants = await participantService.getAllParticipants();
                Table_ParticipantsData1.SuspendLayout();
                emptyTable(Table_ParticipantsData1);
                participantsToolTipHelpers.Clear();
                filteredParticipants = filterParticipantsfromEvent(allParticipants, this.selectedEvent);
                UpdateParticipantsTable(filteredParticipants);
                UpdateCheckedInAndRegistered(filteredParticipants, this.selectedEvent, true);
                Table_ParticipantsData1.ResumeLayout();
            }
        }
        public void emptyTable(TableLayoutPanel table)
        {
            table.RowCount = 1;
            table.Hide();
            table.Controls.Clear();
            table.Show();
            
        }
        public void emptyTableRowsAndColumns(TableLayoutPanel table)
        {

            table.RowStyles.Clear();
            table.Controls.Clear();
            table.RowCount = 1;
            table.ColumnCount = 1;
        }

        private async void Button_UpdateEvents_Click(object sender, EventArgs e)
        {
            events = await eventService.getAllEvents();
            filteredEvents = events;
            Table_EventsData.SuspendLayout();
            UpdateEventsTable(filteredEvents);
            Table_EventsData.ResumeLayout();

        }

        private async void ComboBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table_ParticipantsData1.SuspendLayout();
            emptyTable(Table_ParticipantsData1);
            List<Event> eventsToUpdate = new List<Event>();
            foreach (Event ev in events) {
                Event eventEntity = await updateEventStatus(ev);
                if(eventEntity!=null)
                {
                    eventsToUpdate.Add(eventEntity);
                }
            }
            foreach(Event ev in eventsToUpdate)
            {
                int allEventsIndex = events.FindLastIndex(eve => eve.id.Equals(ev.id));
                int filteredEventsIndex = filteredEvents.FindLastIndex(eve => eve.id.Equals(ev.id));
                if (allEventsIndex >= 0)
                {
                    events[allEventsIndex] = ev;
                }
                if (filteredEventsIndex >= 0)
                {
                    filteredEvents[filteredEventsIndex] = ev;
                }
            }

            this.selectedEvent = events[ComboBox_Events.SelectedIndex];
            filteredParticipants = filterParticipantsfromEvent(allParticipants, this.selectedEvent);
            selectedEventParticipants = filteredParticipants;
            participantsToolTipHelpers.Clear();
            UpdateParticipantsTable(filteredParticipants);
            Table_ParticipantsData1.ResumeLayout();
            UpdateCheckedInAndRegistered(filteredParticipants, this.selectedEvent, true);
            Label_Filtered.Show();
            Label_FilteredAmount.Show();
            Label_FilteredAmount.Text = filteredParticipants.Count.ToString();
            Label_EventDayNumber.Hide();
            int eventDayNumber = eventService.getEventDayNumber(selectedEvent);
            if (eventDayNumber > 0)
            {
                Label_EventDayNumber.Text = "Event day:  " + eventDayNumber.ToString();
                Label_EventDayNumber.Show();
            }
        }

        //Participants filters

        private List<Participant> filterParticipantsfromEvent(List<Participant> participants, Event eventEntity)
        {
            List<Participant> filteredParticipants = participants;
            filteredParticipants = filteredParticipants.FindAll(p => p.eventId.Equals(eventEntity.id.ToString()));

            return filteredParticipants;
        }
        private List<Participant> filterParticipantsfromFirstName(List<Participant> participants, string firstName)
        {
            List<Participant> filteredParticipants = participants;
            if (firstName.Trim().Length > 0)
            {
                filteredParticipants = filteredParticipants.FindAll(p => p.firstName.Trim().Contains(firstName.Trim()));
            }
            return filteredParticipants;
        }
        private List<Participant> filterParticipantsfromLastName(List<Participant> participants, string lastName)
        {
            List<Participant> filteredParticipants = participants;
            if (lastName.Trim().Length > 0)
            {
                filteredParticipants = filteredParticipants.FindAll(p => p.lastName.Trim().Contains(lastName.Trim()));
            }
            return filteredParticipants;
        }
        private List<Participant> filterParticipantsfromCompanyName(List<Participant> participants, string companyName)
        {
            List<Participant> filteredParticipants = participants;
            if (companyName.Replace(" ", "").Length > 0)
            {
                filteredParticipants = filteredParticipants.FindAll(p => p.companyName.Replace(" ", "").Contains(companyName.Replace(" ", "")));
            }
            return filteredParticipants;
        }

        private List<Participant> filterParticipantsfromPaymentStatus(List<Participant> participants, string paymentsStatus)
        {
            List<Participant> filteredParticipants = participants;
            if (paymentsStatus.Length > 0)
            {
                filteredParticipants = filteredParticipants.FindAll(p => p.paymentStatus.Equals(paymentsStatus));
            }
            return filteredParticipants;
        }
        // end of Participant filters
        public void UpdateCheckedInAndRegistered(List<Participant> participants, Event eventEntity, bool loadingData)
        {
            Label_RegisteredAmount.Show();
            Label_Registered.Show();
            Label_RegisteredAmount.Text = participants.Count.ToString();

            if (eventEntity.date_From <= DateHelper.getToday() && eventEntity.date_From.AddDays(Double.Parse((eventEntity.eventLengthDays - 1).ToString())) >= DateHelper.getToday())
            {
                for (int i = 0; i < eventEntity.eventLengthDays; i++)
                {
                    if (eventEntity.date_From.AddDays(Double.Parse(i.ToString())).Equals(DateHelper.getToday()))
                    {
                        regiteredParticipantsTodayNumber = countRegisteredToThisDayParticipants(participants, eventEntity, i + 1);
                        checkedInTodayParticipantsNumber = checkedInToThisDayParticipants(participants, eventEntity, i + 1);
                    }
                }
                Label_CheckedIn.Show();
                Label_CheckedInAmount.Show();
                Label_CheckedInAmount.Text = checkedInTodayParticipantsNumber.ToString() + " / " + regiteredParticipantsTodayNumber.ToString();

            }
            else
            {
                Label_CheckedIn.Hide();
                Label_CheckedInAmount.Hide();
            }
        }
        private async Task<Event> updateEventStatus(Event eventEntity)
        {
            string eventStatus = eventServices.getEventStatus(eventEntity.date_From,
                    eventEntity.eventLengthDays);
            if (!eventEntity.eventStatus.Equals(eventStatus))
            {
                eventEntity.eventStatus = eventStatus;
                Event responseEventEntity = await eventServices.editEvent(eventEntity);
                return responseEventEntity;
                int allEventsIndex = events.FindLastIndex(ev => ev.id.Equals(responseEventEntity.id));
                int filteredEventsIndex = filteredEvents.FindLastIndex(ev => ev.id.Equals(responseEventEntity.id));
                if (allEventsIndex >= 0)
                {
                    events[allEventsIndex] = responseEventEntity;
                }
                if (filteredEventsIndex >= 0)
                {
                    filteredEvents[filteredEventsIndex] = responseEventEntity;
                }
            }
            else
            {
                return null;
            }
        }
        public int countRegisteredToThisDayParticipants(List<Participant> participants, Event eventEntity, int eventDay)
        {
            int participantsCount = 0;
            foreach (Participant participant in participants)
            {
                if (eventDay == 1)
                {
                    if (participant.participateInDay1)
                    {
                        participantsCount = participantsCount + 1;
                    }
                }
                else if (eventDay == 2)
                {
                    if (participant.participateInDay2)
                    {
                        participantsCount = participantsCount + 1;
                    }
                }
                else if (eventDay == 3)
                {
                    if (participant.participateInDay3)
                    {
                        participantsCount = participantsCount + 1;
                    }
                }
                else if (eventDay == 4)
                {
                    if (participant.participateInDay4)
                    {
                        participantsCount = participantsCount + 1;
                    }
                }
            }
            return participantsCount;


        }
        public int checkedInToThisDayParticipants(List<Participant> participants, Event eventEntity, int eventDay)
        {
            int checkedInParticipants = 0;
            foreach (Participant participant in participants)
            {
                if (eventDay == 1)
                {
                    if (participant.checkedInDay1)
                    {
                        checkedInParticipants = checkedInParticipants + 1;
                    }
                }
                else if (eventDay == 2)
                {
                    if (participant.checkedInDay2)
                    {
                        checkedInParticipants = checkedInParticipants + 1;
                    }
                }
                else if (eventDay == 3)
                {
                    if (participant.checkedInDay3)
                    {
                        checkedInParticipants = checkedInParticipants + 1;
                    }
                }
                else if (eventDay == 4)
                {
                    if (participant.checkedInDay4)
                    {
                        checkedInParticipants = checkedInParticipants + 1;
                    }
                }
            }
            return checkedInParticipants;
        }
        private void Button_FilterParticipant_Click(object sender, EventArgs e)
        {
            if (this.selectedEvent != null)
            {
                Button_FilterParticipant.Enabled = false;
                filterParticipants();
                Table_ParticipantsData1.ResumeLayout();
                Button_FilterParticipant.Enabled = true;
            }
        }
        private void filterParticipants()
        {
            
            Table_ParticipantsData1.SuspendLayout();
            emptyTable(Table_ParticipantsData1);
            participantsToolTipHelpers.Clear();
            filteredParticipants = selectedEventParticipants;
            filteredParticipants = filterParticipantsfromFirstName(filteredParticipants, TextBox_FirstNameFilter.Text);
            filteredParticipants = filterParticipantsfromLastName(filteredParticipants, TextBox_LastNameFilter.Text);
            filteredParticipants = filterParticipantsfromCompanyName(filteredParticipants, TextBox_CompanyNameFilter.Text);
            ChangePaymentStatus();

            UpdateParticipantsTable(filteredParticipants);
            Label_FilteredAmount.Text = filteredParticipants.Count.ToString();
        }
        public void ChangePaymentStatus()
        {
            if (ComboBox_PaymentStatus.SelectedIndex >= 0)
            {
                filteredParticipants = filterParticipantsfromPaymentStatus(filteredParticipants, ComboBox_PaymentStatus.SelectedItem.ToString());
                if (ComboBox_PaymentStatus.SelectedItem.ToString().Equals("Paid"))
                {
                    Label_PaidStatus.Show();
                    Label_PaidDueAmount.Show();
                    Label_PaidStatus.Text = "Total paid";
                    Label_PaidDueAmount.Text = participantService.countParticipantsPaymentAmount(filteredParticipants).ToString();
                }
                else if (ComboBox_PaymentStatus.SelectedItem.ToString().Equals("Due"))
                {
                    Label_PaidStatus.Show();
                    Label_PaidDueAmount.Show();
                    Label_PaidStatus.Text = "Total due";
                    Label_PaidDueAmount.Text = participantService.countParticipantsPaymentAmount(filteredParticipants).ToString();
                }
                else if (ComboBox_PaymentStatus.SelectedItem.ToString().Equals("Free"))
                {
                    Label_PaidStatus.Hide();
                    Label_PaidDueAmount.Hide();
                }
            }
            else
            {
                Label_PaidStatus.Hide();
                Label_PaidDueAmount.Hide();
            }
        }
        private Label formatLabel(int size, int width,int height, string text, Color color)
        {
            Label upgradedLabel = new Label();
            upgradedLabel.Width = width;
            upgradedLabel.Height = height;
            upgradedLabel.Text = text;
            Font font = new Font("Segoe UI Semibold", size);
            upgradedLabel.Font = font;
            upgradedLabel.ForeColor = color;
            return upgradedLabel;
        }

        private void Button_ExcelExport_Click(object sender, EventArgs e)
        {
            
            
            if (ComboBox_EventExport.SelectedIndex >= 0)
            {
                DialogResult dialogResult = FolderBrowserDialog_Events.ShowDialog();
                if (dialogResult == DialogResult.OK) {
                    List<Participant> eParticipants = participantService.
                        filterEventParticipants(allParticipants, events[ComboBox_EventExport.SelectedIndex]);
                    GenerateExcel.ReportEventInfo(events[ComboBox_EventExport.SelectedIndex], eParticipants, 
                        FolderBrowserDialog_Events.SelectedPath, 
                        events[ComboBox_EventExport.SelectedIndex].eventName + "_Event_Export");
                }
            }
            else
            {
                messageBoxHelper.showWarning(this, "You have not choosen event", "Warning");
            }
            
        }

        private void Button_DelegateList_Click(object sender, EventArgs e)
        {
            if (ComboBox_EventExport.SelectedIndex >= 0)
            {
                DialogResult dialogResult = FolderBrowserDialog_Events.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    List<Participant> eParticipants = participantService.
                        filterEventParticipants(allParticipants, events[ComboBox_EventExport.SelectedIndex]);
                    GenerateExcel.DelegateAllEventInfo(events[ComboBox_EventExport.SelectedIndex],
                        eParticipants, FolderBrowserDialog_Events.SelectedPath,
                        events[ComboBox_EventExport.SelectedIndex].eventName + "_Delegate_List");
                }
            }
            else
            {
                messageBoxHelper.showWarning(this, "You have not choosen event", "Warning");
            }
        }

        private void Button_ClearParticipantFIlters_Click(object sender, EventArgs e)
        {
            if (this.ComboBox_Events.SelectedIndex >= 0)
            {
                filterWindowData = new FilterWindowData("", "", "", "", "", "", "", "", "", "", "", "", "", false, "", false, false, "", false, false, false, false,
            false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
            false, false);
                TextBox_FirstNameFilter.Text = "";
                TextBox_LastNameFilter.Text = "";
                TextBox_CompanyNameFilter.Text = "";
                ComboBox_PaymentStatus.SelectedIndex = -1;
                filterParticipants();
                Table_ParticipantsData1.ResumeLayout();
            }

        }

        private void Button_FilterEvent_Click(object sender, EventArgs e)
        {
            if (events.Count > 0)
            {
                bool filter = true;
                Button_FilterEvent.Enabled = false;
                filteredEvents = events;
                
                bool warningShown = false;
                int day = 0;
                int month = 0;
                int year = 0;
                bool dayParsed = Int32.TryParse(TextBox_Day.Text, out day);
                bool monthParsed = Int32.TryParse(TextBox_Month.Text, out month);
                bool yearParsed = Int32.TryParse(TextBox_Year.Text, out year);
                if (dayParsed)
                {
                    if (TextBox_Day.Text.Length > 0 && TextBox_Day.Text.Length <= 2 && day <= 31)
                    {
                        filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Day == day);
                        lastEventFilteringDay = day;
                    }
                    else
                    {
                        messageBoxHelper.showWarning(this, "You have entered wrong day (is too high)", "Warning");
                        filter = false;
                    }
                }
                else if (TextBox_Day.Text.Replace(" ", "").Length > 0 && filter)
                {
                    messageBoxHelper.showWarning(this, "You have entered wrong day (contains spaces or contain letters). \n" +
                        "will not be filtered according to days", "Warning");
                    filter = false;
                }
                if (monthParsed && filter)
                {
                    if (TextBox_Month.Text.Length > 0 && TextBox_Month.Text.Length <= 2 && month <= 12)
                    {
                        filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Month == month);
                        lastEventFilteringMonth = month;
                    }
                    else
                    {
                        messageBoxHelper.showWarning(this, "You have entered wrong month (is too high)", "Warning");
                        filter = false;
                    }
                }
                else if (TextBox_Month.Text.Replace(" ", "").Length > 0 && filter)
                {
                    messageBoxHelper.showWarning(this, "You have entered wrong month (contains spaces or contain letters). \n" +
                        "will not be filtered according to month", "Warning");
                    filter = false;
                }
                if (yearParsed && filter)
                {
                    if (TextBox_Year.Text.Length == 4 && year > 1990)
                    {
                        filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Year == year);
                        lastEventFilteringYear = year;
                    }
                    else
                    {
                        messageBoxHelper.showWarning(this, "You have entered wrong year (too high)", "Warning");
                        filter = false;
                    }
                }
                else if (TextBox_Year.Text.Replace(" ", "").Length > 0 && filter) 
                {
                    messageBoxHelper.showWarning(this, "You have entered wrong year (contains spaces or contain letters). \n" +
                        "will not be filtered according to month", "Warning");
                    filter = false;
                }


                if (TextBox_EventName.Text.Replace(" ", "").Length > 0 && filter)
                {
                    filteredEvents = filteredEvents.FindAll(ev => ev.eventName.Contains(TextBox_EventName.Text));
                    lastEventFilteringEventName = TextBox_EventName.Text;
                }
                if (filter)
                {
                    UpdateEventsTable(filteredEvents);
                }
                Button_FilterEvent.Enabled = true;
                Label_FilteredEventsAmount.Text = filteredEvents.Count.ToString();
            }
        }

        private void Button_ChooseColumnsToShow_Click(object sender, EventArgs e)
        {
            if (ComboBox_Events.SelectedIndex >= 0)
            {
                MainWindow_ColumnsToShow window = new MainWindow_ColumnsToShow(this);
                window.Show();
                this.Enabled = false;
            }
            else
            {
                messageBoxHelper.showWarning(this, "You have not choosen event!", "Warning");
            }
        }

        private async void Button_ChooseAdvancedFilters_Click(object sender, EventArgs e)
        {
            if (ComboBox_Events.SelectedIndex >= 0)
            {
                ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
                List<ParticipationFormat> participationFormats = await participationFormatServices.getAllParticipationFormats();
                filterWindowData.firstName = TextBox_FirstNameFilter.Text;
                filterWindowData.lastName = TextBox_LastNameFilter.Text;
                filterWindowData.companyName = TextBox_CompanyNameFilter.Text;
                if (ComboBox_PaymentStatus.SelectedIndex >= 0)
                {
                    filterWindowData.paymentStatus = ComboBox_PaymentStatus.SelectedItem.ToString();
                }
                AllFiltersWindow allFiltersWindow = new AllFiltersWindow(this, selectedEvent, filterWindowData, participationFormats, selectedEventParticipants);
                allFiltersWindow.Show();
                this.Enabled = false;
            }
            else
            {
                messageBoxHelper.showWarning(this, "You have not choosen event!", "Warning");
            }
        
        }

       
    }
}
