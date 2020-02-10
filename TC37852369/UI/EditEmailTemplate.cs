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
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class EditEmailTemplate : MetroForm
    {
        MainWindow mainWindow;
        MailTemplateServices templateServices = new MailTemplateServices();
        List<EmailTemplateString> emailTemplateStrings = new List<EmailTemplateString>();
        List<EmailTemplate> emailTemplates = new List<EmailTemplate>();
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        int lastTemplateId = -1;
        bool justCheckedChanged = false;
        bool creteNewClicked = false;

        TextBox lastTextBoxClicked = null;

        int selectedItemId = -1;
        public EditEmailTemplate(MainWindow window)
        {
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            

            /*for (int i = 0; i < 30; i++)
            {
                this.ListBox_EmailTemplates.Items.Add("Email Template " + i);
            }*/

            TextBox_Subject.Click += TextBoxSubjectClicked;
            TextBox_Body.Click += TextBoxBodyClicked;
            initializeWindowFields();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void TextBoxBodyClicked(object sender, EventArgs e)
        {
            lastTextBoxClicked = TextBox_Body;
        }

        private void TextBoxSubjectClicked(object sender, EventArgs e)
        {
            lastTextBoxClicked = TextBox_Subject;
        }

        public async void initializeWindowFields()
        {
            emailTemplateStrings = await templateServices.getEmailTemplateStrings();
            emailTemplates = await templateServices.getAllMailTemplates();
            for(int i = 0; i < emailTemplateStrings.Count; i++)
            {
                ComboBox_TemplateStrings.Items.Add(emailTemplateStrings[i].name);
            }
            for (int i = 0; i < emailTemplates.Count; i++)
            {
                ListBox_EmailTemplates.Items.Add(emailTemplates[i].templateName);
            }
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

        private void ComboBox_TemplateStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lastTextBoxClicked != null) {
                lastTextBoxClicked.Text = lastTextBoxClicked.Text + emailTemplateStrings[ComboBox_TemplateStrings.SelectedIndex].value;
            }
        }

        private void Button_CreateNew_Click(object sender, EventArgs e)
        {
            
            if (selectedItemId != -1)
            {
                creteNewClicked = true;
                bool emailTemplateChanged = EmailTemplateChanged();
                if (emailTemplateChanged && !justCheckedChanged)
                {
                    if (messageBoxHelper.showYesNo(this, "Are you sure you want to change to other template?", "Warning") == DialogResult.Yes)
                    {
                        selectedItemId = -1;
                        lastTemplateId = -1;
                        ListBox_EmailTemplates.SelectedIndex = -1;
                        TextBox_TemplateName.Text = "";
                        TextBox_Subject.Text = "";
                        TextBox_Body.Text = "";
                        justCheckedChanged = true;
                    }
                    else
                    {
                        justCheckedChanged = true;
                        ListBox_EmailTemplates.SelectedIndex = lastTemplateId;
                    }
                }
                else
                {
                    if (!justCheckedChanged)
                    {
                        selectedItemId = -1;
                        lastTemplateId = -1;
                        ListBox_EmailTemplates.SelectedIndex = -1;
                        TextBox_TemplateName.Text = "";
                        TextBox_Subject.Text = "";
                        TextBox_Body.Text = "";
                        justCheckedChanged = false;
                        creteNewClicked = true;
                    }
                    else
                    {
                        justCheckedChanged = false;
                    }
                }
            }
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            if(selectedItemId >= 0)
            {
                EmailTemplate editedEmailTemlate = await templateServices.editMailTemplate(
                    selectedItemId.ToString(),
                    TextBox_TemplateName.Text,
                    TextBox_Subject.Text,
                    TextBox_Body.Text,
                    false);

                emailTemplates[selectedItemId] = editedEmailTemlate;
            }
            else
            {

                EmailTemplate template = await templateServices.createMailTemplate(
                    TextBox_TemplateName.Text,
                    TextBox_Subject.Text,
                    TextBox_Body.Text,
                    false);
                emailTemplates.Add(template);
                selectedItemId = ListBox_EmailTemplates.Items.Count;
                this.ListBox_EmailTemplates.Items.Add(template.templateName);
                
            }
        }

        private void ListBox_EmailTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool emailTemplateChanged = EmailTemplateChanged();
            if (emailTemplateChanged && !justCheckedChanged)
            {
                if (messageBoxHelper.showYesNo(this, "Are you sure you want to change to other template?", "Warning") == DialogResult.Yes)
                {
                    selectedItemId = ListBox_EmailTemplates.SelectedIndex;
                    lastTemplateId = selectedItemId;
                    TextBox_TemplateName.Text = emailTemplates[selectedItemId].templateName;
                    TextBox_Subject.Text = emailTemplates[selectedItemId].subject;
                    TextBox_Body.Text = emailTemplates[selectedItemId].body;
                    justCheckedChanged = false;
                }
                else
                {
                    justCheckedChanged = true;
                    ListBox_EmailTemplates.SelectedIndex = lastTemplateId;
                }
            }
            else
            {
                if (!justCheckedChanged)
                {
                    if (ListBox_EmailTemplates.SelectedIndex > -1)
                    {
                        selectedItemId = ListBox_EmailTemplates.SelectedIndex;
                        lastTemplateId = selectedItemId;
                        TextBox_TemplateName.Text = emailTemplates[selectedItemId].templateName;
                        TextBox_Subject.Text = emailTemplates[selectedItemId].subject;
                        TextBox_Body.Text = emailTemplates[selectedItemId].body;
                    }
                }
                justCheckedChanged = false;
            }
            if (creteNewClicked)
            {
                creteNewClicked = false;
            }
        }

        public bool EmailTemplateChanged()
        {
            bool bodyChanged = false;
            bool subjectChanged = false;
            bool templateNameChanged = false;
            if (lastTemplateId>= 0)
            {
                bodyChanged = !emailTemplates[lastTemplateId].body.Equals(TextBox_Body.Text);
                subjectChanged = !emailTemplates[lastTemplateId].subject.Equals(TextBox_Subject.Text);
                templateNameChanged = !emailTemplates[lastTemplateId].templateName.Equals(TextBox_TemplateName.Text);
            }
            else
            {
                if (!creteNewClicked)
                {
                    bodyChanged = TextBox_Body.Text.Length > 0;
                    subjectChanged = TextBox_Subject.Text.Length > 0;
                    templateNameChanged = TextBox_TemplateName.Text.Length > 0;
                }
                
            }
            return bodyChanged || subjectChanged || templateNameChanged;
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }
    }
}
