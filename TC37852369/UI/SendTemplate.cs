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
using TC37852369.Services.EmailSending;

namespace TC37852369.UI
{
    public partial class SendTemplate : MetroForm
    {
        MetroForm form;
        List<EmailTemplate> emailTemplates;
        SendEmail sendEmail;
        EmailStringHelper emailStringHelper = new EmailStringHelper();
        CompanyData companyData;
        public List<Participant> participants;
        public List<Event> events;
        public string sendingStatus;
        int currentEmailTemplateIndex = -1;
        ParticipantServices participantServices = new ParticipantServices();
        MainWindow mainWindow;
        GenerateSendInfoWindow generateSendInfoWindow;
        public SendTemplate(List<EmailTemplate> emailTemplates,List<Participant> participants,List<Event>events,MetroForm form
            ,CompanyData companyData,MainWindow mainWindow)
        {
            this.emailTemplates = emailTemplates;
            this.form = form;
            this.participants = participants;
            this.events = events;
            this.companyData = companyData;
            this.mainWindow = mainWindow;
            InitializeComponent();
            loadData();
            sendEmail = new SendEmail(companyData.emailUsername, companyData.emailPassword);
        }

        private void loadData()
        {
           foreach(EmailTemplate t in emailTemplates){
                ComboBox_ChooseTemplate.Items.Add(t.templateName);
            }
        }

        private void ComboBox_ChooseTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox_Subject.Text = emailTemplates[ComboBox_ChooseTemplate.SelectedIndex].subject;
            TextBox_Body.Text = emailTemplates[ComboBox_ChooseTemplate.SelectedIndex].body;

        }

        private async void Button_Send_Click(object sender, EventArgs e)
        {
            currentEmailTemplateIndex = ComboBox_ChooseTemplate.SelectedIndex;
            generateSendInfoWindow = new GenerateSendInfoWindow(this);
            Timer_StringChanged.Enabled = true;
            generateSendInfoWindow.Show();
            var tasks = new[]
                   {
                    Task.Factory.StartNew(() => GenerateDocumentsAndSendEmails())
                    };
            foreach(Task t in tasks)
            {
                await t;
            }
            for (int i =0; i < mainWindow.filteredParticipants.Count; i++)
            {
                mainWindow.editParticipantTableRow(mainWindow.filteredParticipants[i], i);
            }
        }
        private async void GenerateDocumentsAndSendEmails()
        {
            List<Participant> participants = this.participants;
            List<Event> events = this.events;

            
            sendingStatus = "SendingEmails";
            List<string> toEmails = new List<string>();
            List<string> emailBodies = new List<string>();
            List<string> emailSubjects = new List<string>();
            foreach (Participant p in participants)
            {
                toEmails.Add(p.email);
                Event eventEntity = events.Find(ev => ev.id.ToString().Equals(p.eventId));
                string body = "";
                string subject = "";
                if (eventEntity.useTemplate)
                {
                    EmailTemplate temp = emailTemplates.Find(et =>
                    et.templateName.Equals(eventEntity.current_Mail_Template));
                    body = temp.body;
                    subject = temp.subject;
                }
                else
                {
                    body = eventEntity.emailBody;
                    subject = eventEntity.emailSubject;
                }
                emailBodies.Add(emailStringHelper.formatEmailString(body, p, eventEntity));
                emailSubjects.Add(emailStringHelper.formatEmailString(subject, p, eventEntity));
               
            }
            EmailTemplate currentEmailTemplate = emailTemplates[currentEmailTemplateIndex];
            sendEmail.SendEmails(toEmails, emailSubjects, emailBodies);
            sendingStatus = "EmailsSent";
            foreach (Participant p in participants)
            {
                p.ticketSent = true;
                Participant participant = await participantServices.editParticipant(p);

                if (participant != null)
                {
                    int allParticipantsIndex = mainWindow.allParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                    int selectedEventParticipantsIndex = 0;
                    int filteredEventParticipantsIndex = 0;
                    if (mainWindow.selectedEventParticipants.Count > 0)
                    {
                        selectedEventParticipantsIndex = mainWindow.selectedEventParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                        filteredEventParticipantsIndex = mainWindow.filteredParticipants.FindIndex(par => par.participantId.Equals(p.participantId));
                    }
                    mainWindow.allParticipants[allParticipantsIndex] = p;
                    if (mainWindow.selectedEventParticipants.Count > 0)
                    {
                        mainWindow.selectedEventParticipants[selectedEventParticipantsIndex] = p;
                        mainWindow.filteredParticipants[filteredEventParticipantsIndex] = p;
                       
                    }
                }
            }
        }

        

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            form.Enabled = true;
        }

        private void Timer_StringChanged_Tick(object sender, EventArgs e)
        {
            if (sendingStatus.Equals("SendingEmails"))
            {
                generateSendInfoWindow.Timer_Document.Enabled = false;
                generateSendInfoWindow.Timer_Sending.Enabled = true;
                generateSendInfoWindow.Label_Status.Text = "Sending Emails To Participants";
            }
            else if (sendingStatus.Equals("EmailsSent"))
            {
                generateSendInfoWindow.Timer_Sending.Enabled = false;
                generateSendInfoWindow.Timer_Sent.Enabled = true;
                generateSendInfoWindow.Button_Confirm.Enabled = true;
                generateSendInfoWindow.Button_Cancel.Enabled = false;
                generateSendInfoWindow.Label_Status.Text = "Emails    Sent    Successfully";
            }
        }
    }
}
