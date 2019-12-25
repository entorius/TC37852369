using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
public enum EventDuration
{
    oneDay = 1,
    twoDays = 2,
    threeDays = 3,
    fourDays = 4
}
public enum EventStatus
{
    Upcoming ,
    Past

}

namespace TC37852369
{
    public partial class CreateEvent : MetroForm
    {
        MainWindow mainWindow;
        public CreateEvent(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;

            //on initialize add close handler for this form
            this.FormClosed += CloseHandler;

            //on initialize add watermarks on email text fields
            TextBoxModification TextBox_SubjectMod = new TextBoxModification(TextBox_Subject, "Subject", false);
            TextBox_SubjectMod.addEvents();
            TextBoxModification TextBox_BodyMod = new TextBoxModification(TextBox_Body, "Body", false);
            TextBox_BodyMod.addEvents();

            //on initialize fill Combobox of event durations (1,2,3,4 days)
            foreach (EventDuration eventDuration in (EventDuration[])Enum.GetValues(typeof(EventDuration)))
            {
                ComboBox_EventDuration.Items.Add((int)eventDuration);
            }

            //on initialize fill Combobox of event status ( pastEvent, upcomingEvent days)
            foreach (String eventStatus in (String[])Enum.GetNames(typeof(EventStatus)))
            {
                ComboBox_Status.Items.Add(eventStatus);
            }

            //on initialize disable default email templates combobox
            ComboBox_EmailTemplate.Enabled = false;

        }

        private void Button_Create_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
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
    }
}
