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
public enum CompanyTypes
{
    Industry,
    Service,
    Media,
    Government
}
//Delete when all participation formats will be added to thee database
public enum ParticipationFormats
{
     Delegate,
     Exhibitor,
     Speaker,
    [Description("Speaker/Moderator")] SpeakerModerator,
    [Description("Speaker/Chairman")] SpeakerChairman,
    [Description("Strategic Partner")] StrategicPartner,
    [Description("Media Partner")] MediaPartner,
    [Description("Sponsor/Bronze")] SponsorBronze,
    [Description("Sponsor/Silver")] SponsorSilver,
    [Description("Sponsor/Gold")] SponsorGold,
    [Description("Sponsor/Platinum")] SponsorPlatinum,
    [Description("Sponsor/Evening")] SponsorEvening,
    [Description("Sponsor/Identity")] SponsorIdentity,
    [Description("Sponsor/Lunch")] SponsorLunch,
    [Description("Sponsor/Coffee")] SponsorCoffee
}

public enum PaymentStatus
{
   Due,
   Paid,
   Free
}

public enum YesNo
{
    Yes,
    No
}

//Later need to be deleted
public enum Ticket
{
    Sent,
    [Description("Sponsor/Coffee")] NotSent
}


namespace TC37852369
{
    public partial class RegisterParticipant : MetroForm
    {
        MainWindow mainWindow;
        public List<string> participationFormats = new List<string>();
        string addNewParticipationFormat = "+ Add new participation format";
        bool addNewParticipantFormatSelected = false;
        public RegisterParticipant(MainWindow window)
        {
            mainWindow = window;
            this.FormClosed += CloseHandler;
            
            InitializeComponent();
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

            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus);
            }


            foreach (YesNo yesNo in (YesNo[])Enum.GetValues(typeof(YesNo)))
            {
                ComboBox_Materials.Items.Add(yesNo);
                ComboBox_EveningEvent.Items.Add(yesNo);
                ComboBox_TicketSent.Items.Add(yesNo);
                ComboBox_CheckInDayOne.Items.Add(yesNo);
                ComboBox_CheckInDayTwo.Items.Add(yesNo);
            }
            ComboBox_Materials.SelectedIndex = 1;
            ComboBox_EveningEvent.SelectedIndex = 1;
            ComboBox_TicketSent.SelectedIndex = 1;
            ComboBox_CheckInDayOne.SelectedIndex = 1;
            ComboBox_CheckInDayTwo.SelectedIndex = 1;

            ComboBox_PaymentStatus.SelectedIndex = 0;
            ComboBox_ParticipationFormat.Items.Add(addNewParticipationFormat);
            ComboBox_ParticipationFormat.SelectedIndexChanged += ParticipationFormatSelectedIndexChanged;

        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
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
    }
}
