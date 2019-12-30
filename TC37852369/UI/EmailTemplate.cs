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

namespace TC37852369
{
    public partial class EmailTemplate : MetroForm
    {
        MainWindow mainWindow;
        public EmailTemplate(MainWindow window)
        {
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            TextBoxModification TextBox_TemplateNameMod = new TextBoxModification(TextBox_TemplateName, "Template Name", false);
            TextBox_TemplateNameMod.addEvents();
            TextBoxModification TextBox_SubjectMod = new TextBoxModification(TextBox_Subject, "Subject", false);
            TextBox_SubjectMod.addEvents();
            TextBoxModification TextBox_BodyMod = new TextBoxModification(TextBox_Body, "Body", false);
            TextBox_BodyMod.addEvents();

            for (int i = 0; i < 30; i++)
            {
                this.ListBox_EmailTemplates.Items.Add("Email Template " + i);
            }
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
    }
}
