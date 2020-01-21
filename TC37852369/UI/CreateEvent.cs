﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Services;
public enum EventDuration
{
    oneDay = 1,
    twoDays = 2,
    threeDays = 3,
    fourDays = 4
}

namespace TC37852369
{
    public partial class CreateEvent : MetroForm
    {
        MainWindow mainWindow;
        EventServices eventServices = new EventServices();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberService =
            new LastEntityIdentificationNumberServices();
        public CreateEvent(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            //checks acording to how many days the event will last which days dates to let to change
            this.CheckWhichDateTimeFieldsToShow();
            //sets dates according to event begining date and which day of the event it is
            setDaysDates();


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

            setDefaultDateValues(ComboBox_Day1FromHour, ComboBox_Day1ToHour,
                ComboBox_Day1FromMinute, ComboBox_Day1ToMinute);
            setDefaultDateValues(ComboBox_Day2FromHour, ComboBox_Day2ToHour,
                ComboBox_Day2FromMinute, ComboBox_Day2ToMinute);
            setDefaultDateValues(ComboBox_Day3FromHour, ComboBox_Day3ToHour,
                ComboBox_Day3FromMinute, ComboBox_Day3ToMinute);
            setDefaultDateValues(ComboBox_Day4FromHour, ComboBox_Day4ToHour,
                ComboBox_Day4FromMinute, ComboBox_Day4ToMinute);

            //on initialize add close handler for this form
            this.FormClosed += CloseHandler;

            //on initialize fill Combobox of event durations (1,2,3,4 days)
            foreach (EventDuration eventDuration in (EventDuration[])Enum.GetValues(typeof(EventDuration)))
            {
                ComboBox_EventDuration.Items.Add((int)eventDuration);
            }
            ComboBox_EventDuration.SelectedIndex = 0;
            //on initialize disable default email templates combobox
            ComboBox_EmailTemplate.Enabled = false;

        }

        private async void Button_Create_Click(object sender, EventArgs e)
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
            bool isVenueAdressCorrect =  len > 0 ? true : false;

            //Check if email template is correct
            int emailTemplateErrorCode = eventServices.isEmailTemplateCorrect(CheckBox_UseDefaultEmail.Checked, TextBox_Subject.Text,
                TextBox_Body.Text, ComboBox_EmailTemplate.SelectedText);

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

            if (eventErrorCode > 0 || emailTemplateErrorCode > 0 || isSomeTimeFromToNotCorrect >0 
                || !isEventNameCorrect || !isVenueNameCorrect || !isVenueAdressCorrect)
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
                else if(emailTemplateErrorCode > 0)
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
                    ComboBox_Day1FromHour.SelectedItem.ToString() , 
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



                await lastEntityIdentificationNumberService.IncreaseLastIdetificationNumber("Event");
                Event eventEntity = await eventServices.addEvent(TextBox_EventName.Text, DateTime_EventDate.Value,
                   eventDuration, day1, day2, day3, day4,day1TimeFrom, day1TimeTo, 
                   day2TimeFrom, day2TimeTo, day3TimeFrom, day3TimeTo, day4TimeFrom,
                   day4TimeTo, TextBox_VenueName.Text, TextBox_VenueAdress.Text,  
                   eventStatus,TextBox_Comments.Text, CheckBox_UseDefaultEmail.Checked,
                   ComboBox_EmailTemplate.SelectedText, TextBox_Body.Text, TextBox_Subject.Text);
                
                mainWindow.Enabled = true;
                mainWindow.events.Add(eventEntity);
                mainWindow.addEventTableRow();
                mainWindow.addEventToEventTableRow(eventEntity, mainWindow.Table_EventsData.RowCount - 1);
            this.Dispose();
            }

            
        }


        protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }
        //if checkbox use default email changes makes according actions:
        //1)if checked disables subject andd body fields and enables default Email template choise
        //2)if not checked enables subject andd body fields and disables default Email template choise
        private void CheckBox_UseDefaultEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_UseDefaultEmail.Checked)
            {
                TextBox_Subject.Enabled = false;
                TextBox_Body.Enabled = false;
                ComboBox_EmailTemplate.Enabled = true;
            }
            if (!CheckBox_UseDefaultEmail.Checked)
            {
                TextBox_Subject.Enabled = true;
                TextBox_Body.Enabled = true;
                ComboBox_EmailTemplate.Enabled = false;
            }
        }

        private void DateTime_EventDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime_EventDate.Value = SetDateTimeHoursAndMinutes(DateTime_EventDate.Value);
            setDaysDates();
        }
        public void setDaysDates()
        {
            
            DateTime_Day1.Value = DateTime_EventDate.Value;
            DateTime_Day2.Value = DateTime_EventDate.Value.AddDays(1);
            DateTime_Day3.Value = DateTime_EventDate.Value.AddDays(2);
            DateTime_Day4.Value = DateTime_EventDate.Value.AddDays(3);
        }

        private void ComboBox_EventDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckWhichDateTimeFieldsToShow();
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
        public void dayDateShowHide(MetroDateTime day,MetroLabel dayLabel,
            MetroLabel fromLabel,MetroLabel toLabel,MetroComboBox fromHour,
            MetroComboBox fromMinute, MetroComboBox toHour,MetroComboBox toMinute,int dayValue,
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
            for(int i = 0; i <= 23; i++)
            {
                hourComboBox.Items.Add(i);
            }
            for (int i = 0; i <= 59; i++)
            {
                minuteComboBox.Items.Add(i);
            }
        }

        public DateTime SetDateTimeHoursAndMinutes(DateTime date)
        {
            return new DateTime(date.Year,date.Month,date.Day,0,0,0);
        }
        public void setDefaultDateValues(MetroComboBox hourFromComboBox, MetroComboBox hourToComboBox, MetroComboBox minuteFromComboBox, MetroComboBox minuteToComboBox)
        {
            hourFromComboBox.SelectedItem = 0;
            hourToComboBox.SelectedItem = 23;
            minuteFromComboBox.SelectedItem = 0;
            minuteToComboBox.SelectedItem = 59;
        }
        public void showWarning(string text,string type)
        {
            MetroFramework.MetroMessageBox.Show(this, text, type, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


    }
}
