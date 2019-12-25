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
    public partial class CreateUser : MetroForm
    {
        MainWindow mainWindow;
        public CreateUser(MainWindow window)
        {
            this.mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            //on initialiazation adds watermarks on all textFields
            TextBoxModification TextBox_EmailMod = new TextBoxModification(TextBox_Email, "Email", false);
            TextBox_EmailMod.addEvents();

            TextBoxModification TextBox_PasswordMod = new TextBoxModification(TextBox_Password, "Password", true);
            TextBox_PasswordMod.addEvents();

            TextBoxModification TextBox_ConfirmPasswordMod = new TextBoxModification(TextBox_ConfirmPassword, "Confirm password", true);
            TextBox_ConfirmPasswordMod.addEvents();

            TextBoxModification TextBox_NameMod = new TextBoxModification(TextBox_Name, "Name", false);
            TextBox_NameMod.addEvents();

            TextBoxModification TextBox_SurenameMod = new TextBoxModification(TextBox_Surename, "Surename", false);
            TextBox_SurenameMod.addEvents();
        }

        private void Button_CreateUser_Click(object sender, EventArgs e)
        {

        }
        private void Button_Create_Click(object sender, EventArgs e)
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
