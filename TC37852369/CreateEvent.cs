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
    public partial class CreateEvent : MetroForm
    {
        MainWindow mainWindow;
        public CreateEvent(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            this.FormClosed += CloseHandler;
            TextBoxModification TextBox_SubjectMod = new TextBoxModification(TextBox_Subject, "Subject", false);
            TextBox_SubjectMod.addEvents();
            TextBoxModification TextBox_BodyMod = new TextBoxModification(TextBox_Body, "Body", false);
            TextBox_BodyMod.addEvents();
        }

        private void Button_Create_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
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
