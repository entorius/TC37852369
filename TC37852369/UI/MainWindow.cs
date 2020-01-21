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
using MetroFramework.Controls;
using MetroFramework.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI;

namespace TC37852369
{
    public partial class MainWindow : MetroForm
    {
        Login login;
        //events variables
        public EventServices eventService = new EventServices();
        public List<Event> events = new List<Event>();
        public MainWindow(Login login)
        {
            this.login = login;
            this.FormClosed += ClosedHandler;
            InitializeComponent();
            UpdateEventsTable();
        }
       public async void UpdateEventsTable()
        {
            
            events = await eventService.getAllEvents();
            Table_EventsData.RowCount = events.Count;
            for(int i = 0; i < events.Count - 1; i++)
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
            for (int i = 0; i < events.Count; i++)
            {
                addEventToEventTableRow(events[i], i);
            }
            
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

        private void Button_ChangeInformation_Click(object sender, EventArgs e)
        {
            AddChangeCompanyInformation changeInformationWindow = new AddChangeCompanyInformation();
            changeInformationWindow.Show();
        }

        public void addEventTableRow()
        {
            Table_EventsData.RowCount = Table_EventsData.RowCount + 1;
            Table_EventsData.RowStyles.Add(new RowStyle(SizeType.Absolute));
            TableLayoutRowStyleCollection styles =
            Table_EventsData.RowStyles;
            RowStyle styleRow = styles[Table_EventsData.RowCount - 1];
            
            // Set the row height to 20 pixels.
            styleRow.SizeType = SizeType.Absolute;
            styleRow.Height = 40;
            
        }
        public void addEventToEventTableRow(Event eventEntity,int rowNumber)
        {
            Label label_EventName = new Label();
            label_EventName.Text = eventEntity.eventName;
            label_EventName.Width = 300;
            Label label_EventDate = new Label();
            label_EventDate.Text = eventEntity.date_From.ToString("yyyy/MM/dd");
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
            button_Edit.BackColor = ColorTranslator.FromHtml("#626262");
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
            Table_EventsData.GetControlFromPosition( 0, rowNumber).Text = eventEntity.eventName;
            Table_EventsData.GetControlFromPosition(1, rowNumber).Text = eventEntity.date_From.ToString("yyyy/MM/dd");
            Table_EventsData.GetControlFromPosition(2, rowNumber).Text = eventEntity.eventLengthDays.ToString();
            Table_EventsData.GetControlFromPosition(3, rowNumber).Text = eventEntity.venueName;
            Table_EventsData.GetControlFromPosition( 4, rowNumber).Text = eventEntity.venueAdress; ;
            Table_EventsData.GetControlFromPosition( 5, rowNumber).Text = eventEntity.eventStatus; ;
            Table_EventsData.GetControlFromPosition( 6, rowNumber).Text = eventEntity.current_Mail_Template;
            events[rowNumber] = eventEntity;
        }
        private void ButtonEditEventHandler(object sender, EventArgs e)
        {
            var cellPos = GetRowColIndex(
            Table_EventsData,
            Table_EventsData.PointToClient(Cursor.Position));
            EditEvent editEvent = new EditEvent(this, cellPos.Value.Y);
            editEvent.Show();
            this.Enabled = false;
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
    }
}
