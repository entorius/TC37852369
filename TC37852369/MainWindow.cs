using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using TC37852369.Helpers;

namespace TC37852369
{
    public partial class MainWindow : MetroForm
    {
        Login login;
        public MainWindow(Login login)
        {
            this.login = login;
            this.FormClosed += ClosedHandler;
            InitializeComponent();
        }
        public void InitializeEvents()
        {
            
        }

        private void Button_CreateEvent_Click(object sender, EventArgs e)
        {
            CreateEvent cEvent = new CreateEvent(this);
            this.Enabled = false;
            cEvent.Show();
        }

        private void Button_RegisterParticipant_Click(object sender, EventArgs e)
        {
            RegisterParticipant cEvent = new RegisterParticipant(this);
            this.Enabled = false;
            cEvent.Show();
        }
        protected void ClosedHandler(object sender, EventArgs e)
        {
            login.Show();
        }

        private void Button_GenerateMail_Click(object sender, EventArgs e)
        {
            GenerateSend generateSend = new GenerateSend(this);
            generateSend.Show();
            this.Enabled = false;
        }

        private void Button_AddUser_Click(object sender, EventArgs e)
        {
            CreateUser user = new CreateUser(this);
            user.Show();
            this.Enabled = false;
        }

        private void Button_GenerateTicket_Click(object sender, EventArgs e)
        {
            GenerateTicket ticket = new GenerateTicket(this);
            ticket.Show();
            this.Enabled = false;
        }

        private void Button_EditEmail_Click(object sender, EventArgs e)
        {
            EmailTemplate email = new EmailTemplate(this);
            email.Show();
            this.Enabled = false;
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            GenerateExcel generator = new GenerateExcel();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            generator.ExportEventInfo("Event Name", projectDirectory, "YeetForLife");
        }
    }
}
