using MetroFramework.Controls;
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

namespace TC37852369.UI
{
    public partial class EditEvent : MetroForm
    {
        MainWindow mainWindow;
        EventServices eventServices = new EventServices();
        MailTemplateServices mailTeplateServices = new MailTemplateServices();

        List<EmailTemplate> mailTemplates = new List<EmailTemplate>();
        List<EmailTemplateString> mailTemplateStrings = new List<EmailTemplateString>();
        TextBox lastSelectedMailTextBox = null;
        long eventId;
        string lastEmailBody = "";
        string lastEmailSubject = "";
        public EditEvent(MainWindow window, long eventId)
        {
            mainWindow = window;
            InitializeComponent();

            this.FormClosed += CloseHandler;
            this.eventId = eventId;
           

            TextBox_Body.Click += TextBox_BodyClicked;
            TextBox_Subject.Click += TextBox_SubjectClicked;

            fillWindowFields();
            setDaysDates();

            this.CheckWhichDateTimeFieldsToShow();
            //on initialize disable default email templates combobox
            ComboBox_EmailTemplate.Enabled = false;
        }

        // Fills window fields on initialization
        public async void fillWindowFields()
        {
            DateTime_EventDate.Value = SetDateTimeHoursAndMinutes(DateTime_EventDate.Value);
            //fills hours and minutes comboboxes of the days
            fillHourMinuteComboBox(ComboBox_Day1FromHour, ComboBox_Day1FromMinute);
            fillHourMinuteComboBox(ComboBox_Day1ToHour, ComboBox_Day1ToMinute);
            fillHourMinuteComboBox(ComboBox_Day2FromHour, ComboBox_Day2FromMinute);
            fillHourMinuteComboBox(ComboBox_Day2ToHour, ComboBox_Day2ToMinute);
            fillHourMinuteComboBox(ComboBox_Day3FromHour, ComboBox_Day3FromMinute);
            fillHourMinuteComboBox(ComboBox_Day3ToHour, ComboBox_Day3ToMinute);
            fillHourMinuteComboBox(ComboBox_Day4FromHour, ComboBox_Day4FromMinute);
            fillHourMinuteComboBox(ComboBox_Day4ToHour, ComboBox_Day4ToMinute);

            //on initialize fill Combobox of event durations (1,2,3,4 days)
            foreach (EventDuration eventDuration in (EventDuration[])Enum.GetValues(typeof(EventDuration)))
            {
                ComboBox_EventDuration.Items.Add((int)eventDuration);
            }
            ComboBox_EventDuration.SelectedIndex = 0;
            mailTemplates = await mailTeplateServices.getAllMailTemplates();
            mailTemplateStrings = await mailTeplateServices.getEmailTemplateStrings();
            for (int i = 0; i < mailTemplates.Count; i++)
            {
                ComboBox_EmailTemplate.Items.Add(mailTemplates[i].templateName);
            }
            for (int i = 0; i < mailTemplateStrings.Count; i++)
            {
                ComboBox_TemplateStrings.Items.Add(mailTemplateStrings[i].name);
            }
            fillFieldsWithEventData(mainWindow.events.FindLast(delegate (Event eventEntity)
            {
                return eventEntity.id == eventId;
            }));
        }

        private void TextBox_SubjectClicked(object sender, EventArgs e)
        {
            lastSelectedMailTextBox = TextBox_Subject;
        }

        private void TextBox_BodyClicked(object sender, EventArgs e)
        {
            lastSelectedMailTextBox = TextBox_Body;
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            int eventDuration = Int32.Parse(ComboBox_EventDuration.SelectedItem.ToString());

            //Check if event dates are correct
            int eventErrorCode = eventServices.isEventDatesCorrect(DateTime_EventDate.Value,
                eventDuration, DateTime_Day1.Value, DateTime_Day2.Value, DateTime_Day3.Value,
                DateTime_Day4.Value);

            //Check if event details strings are correct
            bool isEventNameCorrect = eventServices.isStringCorrect(TextBox_EventName.Text);
            bool isVenueNameCorrect = eventServices.isStringCorrect(TextBox_VenueName.Text);
            int len = TextBox_VenueAdress.Text.Length;
            bool isVenueAdressCorrect = len > 0 ? true : false;

            string emailTemplateString = "";
            if(ComboBox_EmailTemplate.SelectedIndex >=0)
            {
                emailTemplateString = mailTemplates[ComboBox_EmailTemplate.SelectedIndex].templateName;
            }
            //Check if email template is correct
            int emailTemplateErrorCode = eventServices.isEmailTemplateCorrect(CheckBox_UseDefaultEmail.Checked, TextBox_Subject.Text,
                TextBox_Body.Text, emailTemplateString);

            //check if all From and To times are correct
            int isSomeTimeFromToNotCorrect = eventServices.isSomeTimeFromToNotCorrect(
                Int32.Parse(ComboBox_Day1FromHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day1FromMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day1ToHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day1ToMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day2FromHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day2FromMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day2ToHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day2ToMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day3FromHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day3FromMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day3ToHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day3ToMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day4FromHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day4FromMinute.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day4ToHour.SelectedItem.ToString()),
                Int32.Parse(ComboBox_Day4ToMinute.SelectedItem.ToString()),
                eventDuration
                );

            int checkIfPaymentAmountCorect = eventServices.checkIfPaymentAmountCorrect(TextBox_PaymentAmount.Text);

            if (eventErrorCode > 0 || emailTemplateErrorCode > 0 || isSomeTimeFromToNotCorrect > 0
                || checkIfPaymentAmountCorect > 0 || !isEventNameCorrect || !isVenueNameCorrect
                || !isVenueAdressCorrect)
            {
                if (!isEventNameCorrect)
                {
                    showWarning("Event name is too short (minimum 3 letters)", "Warning");
                }
                else if (!isVenueNameCorrect)
                {
                    showWarning("Venue name is too short (minimum 3 letters)", "Warning");
                }
                else if (!isVenueAdressCorrect)
                {
                    showWarning("Venue address is too short (minimum 1 letters or number)", "Warning");
                }
                else if (eventErrorCode > 0)
                {
                    if (eventErrorCode == 5)
                    {
                        showWarning("Event duration not choosen", "Warning");
                    }
                    else if (eventErrorCode == 1)
                    {
                        showWarning("Event day " + eventErrorCode + " date is not equal to " +
                            "event begining day",
                            "Warning");
                    }
                    else
                    {
                        showWarning("Event day " + eventErrorCode + " date is earlier than " +
                            "event day" + (eventErrorCode - 1) + " date",
                            "Warning");
                    }
                }
                else if (checkIfPaymentAmountCorect > 0)
                {
                    if (checkIfPaymentAmountCorect == 1)
                    {
                        showWarning("Event Payment Amount for day not entered", "Warning");
                    }
                    else if (checkIfPaymentAmountCorect == 2)
                    {
                        showWarning("Event Payment Amount for day entered in incorrect format",
                            "Warning");
                    }
                    else if (checkIfPaymentAmountCorect == 3)
                    {
                        showWarning("Event Payment Amount for day is too high (maximum value" +
                            "1.79 * 10^308",
                            "Warning");
                    }
                }
                else if (emailTemplateErrorCode > 0)
                {
                    if (emailTemplateErrorCode == 1)
                    {
                        showWarning("Email template is not choosen", "Warning");
                    }
                    else if (emailTemplateErrorCode == 2)
                    {
                        showWarning("Email subject is too short " +
                            "(must be at least 5 characters length)",
                            "Warning");
                    }
                    else if (emailTemplateErrorCode == 3)
                    {
                        showWarning("Email body is too short " +
                            "(must be at least 5 characters length)",
                            "Warning");
                    }
                }
                else if (isSomeTimeFromToNotCorrect > 0)
                {
                    showWarning("Day " + isSomeTimeFromToNotCorrect + " time from is " +
                        "bigger or equal to time to",
                            "Warning");
                }
            }
            else
            {
                string eventStatus = eventServices.getEventStatus(DateTime_EventDate.Value,
                    eventDuration);
                DateTime day1 = SetDateTimeHoursAndMinutes(DateTime_Day1.Value);
                DateTime day2 = SetDateTimeHoursAndMinutes(DateTime_Day2.Value);
                DateTime day3 = SetDateTimeHoursAndMinutes(DateTime_Day3.Value);
                DateTime day4 = SetDateTimeHoursAndMinutes(DateTime_Day4.Value);
                string day1TimeFrom = eventServices.formHoursAndMinutesString(
                    ComboBox_Day1FromHour.SelectedItem.ToString(),
                    ComboBox_Day1FromMinute.SelectedItem.ToString());
                string day1TimeTo = eventServices.formHoursAndMinutesString(
                    ComboBox_Day1ToHour.SelectedItem.ToString(),
                    ComboBox_Day1ToMinute.SelectedItem.ToString());

                string day2TimeFrom = eventServices.formHoursAndMinutesString(
                    ComboBox_Day2FromHour.SelectedItem.ToString(),
                    ComboBox_Day2FromMinute.SelectedItem.ToString());
                string day2TimeTo = eventServices.formHoursAndMinutesString(
                    ComboBox_Day2ToHour.SelectedItem.ToString(),
                    ComboBox_Day2ToMinute.SelectedItem.ToString());

                string day3TimeFrom = eventServices.formHoursAndMinutesString(
                    ComboBox_Day3FromHour.SelectedItem.ToString(),
                    ComboBox_Day3FromMinute.SelectedItem.ToString());
                string day3TimeTo = eventServices.formHoursAndMinutesString(
                    ComboBox_Day3ToHour.SelectedItem.ToString(),
                    ComboBox_Day3ToMinute.SelectedItem.ToString());

                string day4TimeFrom = eventServices.formHoursAndMinutesString(
                    ComboBox_Day4FromHour.SelectedItem.ToString(),
                    ComboBox_Day4FromMinute.SelectedItem.ToString());
                string day4TimeTo = eventServices.formHoursAndMinutesString(
                    ComboBox_Day4ToHour.SelectedItem.ToString(),
                    ComboBox_Day4ToMinute.SelectedItem.ToString());

                double paymentAmountForDay = Double.Parse(TextBox_PaymentAmount.Text);

                string emailTemplate = "";
                string emailBody = TextBox_Body.Text;
                string emailSubject = TextBox_Subject.Text;
                if (CheckBox_UseDefaultEmail.Checked)
                {

                    emailTemplate = mailTemplates[ComboBox_EmailTemplate.SelectedIndex].templateName;
                    emailBody = "";
                    emailSubject = "";
                }

                Event eventEntity = await eventServices.editEvent(this.eventId.ToString(),
                    TextBox_EventName.Text, DateTime_EventDate.Value,
                   eventDuration, day1, day2, day3, day4, day1TimeFrom, day1TimeTo,
                   day2TimeFrom, day2TimeTo, day3TimeFrom, day3TimeTo, day4TimeFrom,
                   day4TimeTo,TextBox_WebPage.Text, paymentAmountForDay, TextBox_VenueName.Text, TextBox_VenueAdress.Text,
                   eventStatus, TextBox_Comments.Text, CheckBox_UseDefaultEmail.Checked,
                   emailTemplate, emailBody, emailSubject);
                if (eventEntity != null)
                {
                    mainWindow.Enabled = true;
                    int eventIndex = mainWindow.events.FindLastIndex(delegate (Event eventEnt)
                    {
                        return eventEnt.id == eventId;
                    });
                    mainWindow.editEventTableRow(eventEntity, eventIndex);
                    this.Dispose();
                }
                else
                {
                    showWarning("Event creation was unsuccesfull, because of internet connection" +
                        "or database write request number exceeded", "Warning");
                }
            }
        }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }

        private void ComboBox_EventDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime_EventDate.Value = SetDateTimeHoursAndMinutes(DateTime_EventDate.Value);
            CheckWhichDateTimeFieldsToShow();
        }

        private void DateTime_EventDate_ValueChanged(object sender, EventArgs e)
        {
            setDaysDates();
        }

        private void CheckBox_UseDefaultEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_UseDefaultEmail.Checked)
            {
                
                ComboBox_EmailTemplate.Enabled = true;
                lastEmailBody = TextBox_Body.Text;
                lastEmailSubject = TextBox_Subject.Text;
                if (ComboBox_EmailTemplate.SelectedIndex >= 0)
                {
                    int index = ComboBox_EmailTemplate.SelectedIndex;
                    EmailTemplate selectedTemplate = mailTemplates[index];
                    TextBox_Subject.Text = selectedTemplate.subject;
                    TextBox_Body.Text = selectedTemplate.body;
                }
                TextBox_Subject.Enabled = false;
                TextBox_Body.Enabled = false;
                ComboBox_TemplateStrings.Enabled = false;
            }
            if (!CheckBox_UseDefaultEmail.Checked)
            {
                TextBox_Subject.Enabled = true;
                TextBox_Body.Enabled = true;
                TextBox_Subject.Text = lastEmailSubject;
                TextBox_Body.Text = lastEmailBody;
                ComboBox_EmailTemplate.Enabled = false;
                ComboBox_TemplateStrings.Enabled = true;
            }
            
        }
        public void setDaysDates()
        {

            DateTime_Day1.Value = DateTime_EventDate.Value;
            DateTime_Day2.Value = DateTime_EventDate.Value.AddDays(1);
            DateTime_Day3.Value = DateTime_EventDate.Value.AddDays(2);
            DateTime_Day4.Value = DateTime_EventDate.Value.AddDays(3);
        }
        public void CheckWhichDateTimeFieldsToShow()
        {
            int? comboboxValue = ComboBox_EventDuration.SelectedItem as int?;
            dayDateShowHide(DateTime_Day1, Label_Day1, Label_From1, Label_To1, ComboBox_Day1FromHour,
                ComboBox_Day1FromMinute, ComboBox_Day1ToHour, ComboBox_Day1ToMinute, 0, comboboxValue);

            dayDateShowHide(DateTime_Day2, Label_Day2, Label_From2, Label_To2, ComboBox_Day2FromHour,
                ComboBox_Day2FromMinute, ComboBox_Day2ToHour, ComboBox_Day2ToMinute, 1, comboboxValue);

            dayDateShowHide(DateTime_Day3, Label_Day3, Label_From3, Label_To3, ComboBox_Day3FromHour,
                ComboBox_Day3FromMinute, ComboBox_Day3ToHour, ComboBox_Day3ToMinute, 2, comboboxValue);

            dayDateShowHide(DateTime_Day4, Label_Day4, Label_From4, Label_To4, ComboBox_Day4FromHour,
                ComboBox_Day4FromMinute, ComboBox_Day4ToHour, ComboBox_Day4ToMinute, 3, comboboxValue);
        }
        public void dayDateShowHide(MetroDateTime day, MetroLabel dayLabel,
            MetroLabel fromLabel, MetroLabel toLabel, MetroComboBox fromHour,
            MetroComboBox fromMinute, MetroComboBox toHour, MetroComboBox toMinute, int dayValue,
            int? comboBoxValue)
        {
            if (comboBoxValue != null)
            {
                if (comboBoxValue > dayValue)
                {
                    day.Show();
                    dayLabel.Show();
                    fromLabel.Show();
                    toLabel.Show();
                    fromHour.Show();
                    fromMinute.Show();
                    toHour.Show();
                    toMinute.Show();
                }
                else
                {
                    day.Hide();
                    dayLabel.Hide();
                    fromLabel.Hide();
                    toLabel.Hide();
                    fromHour.Hide();
                    fromMinute.Hide();
                    toHour.Hide();
                    toMinute.Hide();
                }
            }
            else
            {
                day.Hide();
                dayLabel.Hide();
                fromLabel.Hide();
                toLabel.Hide();
                fromHour.Hide();
                fromMinute.Hide();
                toHour.Hide();
                toMinute.Hide();
            }
        }
        public void fillHourMinuteComboBox(MetroComboBox hourComboBox, MetroComboBox minuteComboBox)
        {
            for (int i = 0; i <= 23; i++)
            {
                hourComboBox.Items.Add(i);
            }
            for (int i = 0; i <= 59; i++)
            {
                minuteComboBox.Items.Add(i);
            }
        }
        public void showWarning(string text, string type)
        {
            MetroFramework.MetroMessageBox.Show(this, text, type, MessageBoxButtons.OK, MessageBoxIcon.Warning);
         }
        public DateTime SetDateTimeHoursAndMinutes(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
        public void setDefaultDateValues(MetroComboBox hourFromComboBox, MetroComboBox hourToComboBox, MetroComboBox minuteFromComboBox, MetroComboBox minuteToComboBox)
        {
            hourFromComboBox.SelectedItem = 0;
            hourToComboBox.SelectedItem = 23;
            minuteFromComboBox.SelectedItem = 0;
            minuteToComboBox.SelectedItem = 59;
        }

        public void fillFieldsWithEventData(Event eventEntity){
            TextBox_EventName.Text = eventEntity.eventName;
            DateTime_EventDate.Value = eventEntity.date_From;
            ComboBox_EventDuration.SelectedIndex = eventEntity.eventLengthDays - 1;
            DateTime_Day1.Value = eventEntity.day1Date;
            DateTime_Day2.Value = eventEntity.day2Date;
            DateTime_Day3.Value = eventEntity.day3Date;
            DateTime_Day4.Value = eventEntity.day4Date;

            ComboBox_Day1FromHour.SelectedIndex = Int32.Parse(eventEntity.day1TimeFrom.Split(':')[0]);
            ComboBox_Day1FromMinute.SelectedIndex = Int32.Parse(eventEntity.day1TimeFrom.Split(':')[1]);
            ComboBox_Day1ToHour.SelectedIndex = Int32.Parse(eventEntity.day1TimeTo.Split(':')[0]);
            ComboBox_Day1ToMinute.SelectedIndex = Int32.Parse(eventEntity.day1TimeTo.Split(':')[1]);

            ComboBox_Day2FromHour.SelectedIndex = Int32.Parse(eventEntity.day2TimeFrom.Split(':')[0]);
            ComboBox_Day2FromMinute.SelectedIndex = Int32.Parse(eventEntity.day2TimeFrom.Split(':')[1]);
            ComboBox_Day2ToHour.SelectedIndex = Int32.Parse(eventEntity.day2TimeTo.Split(':')[0]);
            ComboBox_Day2ToMinute.SelectedIndex = Int32.Parse(eventEntity.day2TimeTo.Split(':')[1]);

            ComboBox_Day3FromHour.SelectedIndex = Int32.Parse(eventEntity.day3TimeFrom.Split(':')[0]);
            ComboBox_Day3FromMinute.SelectedIndex = Int32.Parse(eventEntity.day3TimeFrom.Split(':')[1]);
            ComboBox_Day3ToHour.SelectedIndex = Int32.Parse(eventEntity.day3TimeTo.Split(':')[0]);
            ComboBox_Day3ToMinute.SelectedIndex = Int32.Parse(eventEntity.day3TimeTo.Split(':')[1]);

            ComboBox_Day4FromHour.SelectedIndex = Int32.Parse(eventEntity.day4TimeFrom.Split(':')[0]);
            ComboBox_Day4FromMinute.SelectedIndex = Int32.Parse(eventEntity.day4TimeFrom.Split(':')[1]);
            ComboBox_Day4ToHour.SelectedIndex = Int32.Parse(eventEntity.day4TimeTo.Split(':')[0]);
            ComboBox_Day4ToMinute.SelectedIndex = Int32.Parse(eventEntity.day4TimeTo.Split(':')[1]);

            TextBox_VenueName.Text = eventEntity.venueName;
            TextBox_VenueAdress.Text = eventEntity.venueAdress;

            TextBox_PaymentAmount.Text = eventEntity.paymentAmountForDay.ToString();

            TextBox_Comments.Text = eventEntity.comment;
            TextBox_Subject.Text = eventEntity.emailSubject;
            TextBox_Body.Text = eventEntity.emailBody;
            CheckBox_UseDefaultEmail.Checked = eventEntity.useTemplate;
            if (eventEntity.current_Mail_Template.Length > 0) {
                int indexOfEmailTemplate = ComboBox_EmailTemplate.Items.IndexOf(eventEntity.current_Mail_Template);
                ComboBox_EmailTemplate.SelectedIndex = indexOfEmailTemplate;
            }
            

        }
        protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void ComboBox_EmailTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox_Subject.Enabled = true;
            TextBox_Body.Enabled = true;

            EmailTemplate selectedMailTemplate = mailTemplates[ComboBox_EmailTemplate.SelectedIndex];
            TextBox_Subject.Text = selectedMailTemplate.subject;
            TextBox_Body.Text = selectedMailTemplate.body;

            TextBox_Subject.Enabled = false;
            TextBox_Body.Enabled = false;
        }

        private void ComboBox_TemplateStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastSelectedMailTextBox != null)
            {
                lastSelectedMailTextBox.Text = lastSelectedMailTextBox.Text + mailTemplateStrings[ComboBox_TemplateStrings.SelectedIndex].value;
            }
        }
    }
}
