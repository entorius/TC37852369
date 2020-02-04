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
using TC37852369.UI.helpers;

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
        Dictionary<int,Image> eventImages = new Dictionary<int,Image>();
        Dictionary<int, string> eventImagePath = new Dictionary<int, string>();
        MailTemplateServices mailTeplateServices = new MailTemplateServices();

        List<EmailTemplate> mailTemplates = new List<EmailTemplate>();
        List<EmailTemplateString> mailTemplateStrings = new List<EmailTemplateString>();
        TextBox lastSelectedMailTextBox = null;
        string lastEmailBody = "";
        string lastEmailSubject = "";

        public CreateEvent(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            //checks acording to how many days the event will last which days dates to let to change
            this.CheckWhichDateTimeFieldsToShow();
            //sets dates according to event begining date and which day of the event it is
            setDaysDates();
            updateImageButtons("",0);

            TextBox_Body.Click += TextBox_BodyClicked;
            TextBox_Subject.Click += TextBox_SubjectClicked;

            //on initialize add close handler for this form
            this.FormClosed += CloseHandler;

            fillWindowFields();

            //on initialize disable default email templates combobox
            ComboBox_EmailTemplate.Enabled = false;

        }

        private void TextBox_SubjectClicked(object sender, EventArgs e)
        {
            lastSelectedMailTextBox = TextBox_Subject;
        }

        private void TextBox_BodyClicked(object sender, EventArgs e)
        {
            lastSelectedMailTextBox = TextBox_Body;
        }

        private async void fillWindowFields()
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

            setDefaultDateValues(ComboBox_Day1FromHour, ComboBox_Day1ToHour,
                ComboBox_Day1FromMinute, ComboBox_Day1ToMinute);
            setDefaultDateValues(ComboBox_Day2FromHour, ComboBox_Day2ToHour,
                ComboBox_Day2FromMinute, ComboBox_Day2ToMinute);
            setDefaultDateValues(ComboBox_Day3FromHour, ComboBox_Day3ToHour,
                ComboBox_Day3FromMinute, ComboBox_Day3ToMinute);
            setDefaultDateValues(ComboBox_Day4FromHour, ComboBox_Day4ToHour,
                ComboBox_Day4FromMinute, ComboBox_Day4ToMinute);

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
            bool isVenueAdressCorrect = len > 0 ? true : false;

            string emailTemplateString = "";
            if (ComboBox_EmailTemplate.SelectedIndex >= 0)
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
                else if(checkIfPaymentAmountCorect > 0)
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
                
                /*bool imageExists = eventImagePath.ContainsKey(1);
                if (imageExists)
                {
                    string path = "";
                    eventImagePath.TryGetValue(1, out path);
                    eventServices.addEventImage(path);
                }*/
                await lastEntityIdentificationNumberService.IncreaseLastIdetificationNumber("Event");
                Event eventEntity = await eventServices.addEvent(TextBox_EventName.Text, DateTime_EventDate.Value,
                   eventDuration, day1, day2, day3, day4, day1TimeFrom, day1TimeTo,
                   day2TimeFrom, day2TimeTo, day3TimeFrom, day3TimeTo, day4TimeFrom,
                   day4TimeTo,TextBox_WebPage.Text, paymentAmountForDay, TextBox_VenueName.Text, TextBox_VenueAdress.Text,
                   eventStatus, TextBox_Comments.Text, CheckBox_UseDefaultEmail.Checked,
                   emailTemplate, emailBody, emailSubject);
                if (eventEntity != null)
                {
                    mainWindow.Enabled = true;
                    mainWindow.ComboBox_Events.Items.Add(eventEntity.eventName);
                    mainWindow.events.Add(eventEntity);
                    mainWindow.addEventTableRow();
                    mainWindow.addEventToEventTableRow(eventEntity, mainWindow.Table_EventsData.RowCount - 1);
                    this.Dispose();
                }
                else
                {
                    showWarning("Event creation was unsuccesfull, because of internet connection" +
                        "or database write request number exceeded", "Warning");
                }


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
            MetroMessageBoxHelper helper = new MetroMessageBoxHelper();
            helper.showWarning(this, text, type);
        }

        private void Button_AddImage_Click(object sender, EventArgs e)
        {
            FileDialog_AddEvent.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (FileDialog_AddEvent.ShowDialog() == DialogResult.OK)
            {
                string fileName = FileDialog_AddEvent.FileName;
                Image image = Image.FromFile(fileName);
                string[] imageName = System.Text.RegularExpressions.Regex.Split(fileName, @"\\");
                int updatingImageNumber = 0;
                if (eventImages.Count < 5)
                {
                    bool added = false;
                    for(int i = 1; i <= 5; i++)
                    {
                        if (!eventImages.ContainsKey(i) && !added)
                        {
                            eventImages.Add(i, image);
                            eventImagePath.Add(i, fileName);
                            updatingImageNumber = i;
                            added = true;
                        }
                    }
                    
                    updateImageButtons(imageName.Last(), updatingImageNumber);
                }
                else
                {
                    showWarning("You can add maximum 5 images. ", "Warning");
                }
            }
        }
        public void updateImageButtons(string imageName, int updatingImageNumber)
        {
            updateImageButton(eventImages.ContainsKey(1), Button_Image1, Button_Delete1,imageName,updatingImageNumber, 1);
            updateImageButton(eventImages.ContainsKey(2), Button_Image2, Button_Delete2, imageName, updatingImageNumber, 2);
            updateImageButton(eventImages.ContainsKey(3), Button_Image3, Button_Delete3, imageName, updatingImageNumber, 3);
            updateImageButton(eventImages.ContainsKey(4), Button_Image4, Button_Delete4, imageName, updatingImageNumber, 4);
            updateImageButton(eventImages.ContainsKey(5), Button_Image5, Button_Delete5, imageName, updatingImageNumber, 5);
        }
        public void updateImageButton(bool eventImagesContainsButtonNumber, Button button, PictureBox pictureBox,
            string imageName, int updatingNumber, int key)
        {
            if (eventImagesContainsButtonNumber)
            {
                pictureBox.Show();
                button.Show();
                if(updatingNumber == key)
                {
                    button.Text = imageName;
                }
                
            }
            else
            {
                button.Hide();
                pictureBox.Hide();
            }
        }

        private void Button_Image1_Click(object sender, EventArgs e)
        {
            SaveImageDialogAction(1, Button_Image1);
            
        }

        private void Button_Image2_Click(object sender, EventArgs e)
        {
            SaveImageDialogAction(2, Button_Image2);
        }

        private void Button_Image3_Click(object sender, EventArgs e)
        {
            SaveImageDialogAction(3, Button_Image3);
        }

        private void Button_Image4_Click(object sender, EventArgs e)
        {
            SaveImageDialogAction(4, Button_Image4);
        }

        private void Button_Image5_Click(object sender, EventArgs e)
        {
            SaveImageDialogAction(5, Button_Image5);
        }
        public void SaveImageDialogAction(int buttonNumber, Button button)
        {
            if (folderBrowserDialog_ImageSave.ShowDialog() == DialogResult.OK)
            {
                string ImageFolderName = folderBrowserDialog_ImageSave.SelectedPath;
                Image imageToSave;
                eventImages.TryGetValue(buttonNumber, out imageToSave);
                SaveImage(imageToSave, ImageFolderName, button.Text);
            }
        }
        public void SaveImage(Image image, string path, string imageName)
        {
            string fullImagePath = path + @"\" + imageName;
            image.Save(fullImagePath);
        }

        private void Button_Delete1_Click(object sender, EventArgs e)
        {
            eventImages.Remove(1);
            eventImagePath.Remove(1);
            updateImageButtons("", 0);
        }

        private void Button_Delete2_Click(object sender, EventArgs e)
        {
            eventImages.Remove(2);
            eventImagePath.Remove(2);
            updateImageButtons("", 0);
        }

        private void Button_Delete3_Click(object sender, EventArgs e)
        {
            eventImages.Remove(3);
            eventImagePath.Remove(3);
            updateImageButtons("", 0);
        }

        private void Button_Delete4_Click(object sender, EventArgs e)
        {
            eventImages.Remove(4);
            eventImagePath.Remove(4);
            updateImageButtons("", 0);
        }

        private void Button_Delete5_Click(object sender, EventArgs e)
        {
            eventImages.Remove(5);
            eventImagePath.Remove(5);
            updateImageButtons("", 0);
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
