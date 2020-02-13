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
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class CreateUser : MetroForm
    {
        MainWindow mainWindow;
        UserServices userServices = new UserServices();
        MetroMessageBoxHelper metroMessageBoxHelper = new MetroMessageBoxHelper();
        public CreateUser(MainWindow window)
        {
            this.mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            //on initialiazation adds watermarks on all textFields

            TextBoxModification TextBox_UsernameMod = new TextBoxModification(TextBox_Username, "Username", false);
            TextBox_UsernameMod.addEvents();

            TextBoxModification TextBox_PasswordMod = new TextBoxModification(TextBox_Password, "Password", true);
            TextBox_PasswordMod.addEvents();

            TextBoxModification TextBox_ConfirmPasswordMod = new TextBoxModification(TextBox_ConfirmPassword, "Confirm password", true);
            TextBox_ConfirmPasswordMod.addEvents();

            
            TextBoxModification TextBox_EmailMod = new TextBoxModification(TextBox_Email, "Email", false);
            TextBox_EmailMod.addEvents();

            TextBoxModification TextBox_NameMod = new TextBoxModification(TextBox_Name, "Name", false);
            TextBox_NameMod.addEvents();

            TextBoxModification TextBox_SurenameMod = new TextBoxModification(TextBox_Surename, "Surename", false);
            TextBox_SurenameMod.addEvents();

            TextBoxModification TextBox_PhoneNumberMod = new TextBoxModification(TextBox_PhoneNumber, "Phone Number", false);
            TextBox_PhoneNumberMod.addEvents();

            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void Button_CreateUser_Click(object sender, EventArgs e)
        {

        }
        private async void Button_Create_Click(object sender, EventArgs e)
        {
            Button_Create.Enabled = false;
            string username = TextBox_Username.Text;
            string password = TextBox_Password.Text;
            string confirmPassword = TextBox_ConfirmPassword.Text;
            string name = TextBox_Name.Text;
            string surename = TextBox_Surename.Text;
            string phoneNumber = TextBox_PhoneNumber.Text;
            string email = TextBox_Email.Text;
            bool usernameCorrect = userServices.isUsernameCorrect(username);
            bool passwordCorrect = userServices.isPasswodCorrect(password, confirmPassword);
            if (!usernameCorrect)
            {
                metroMessageBoxHelper.showWarning(this, "Username is too short (must be at least 6 characters long)", "Warning");
            }
            else if (!passwordCorrect)
            {
                metroMessageBoxHelper.showWarning(this, "Passwords not matching or password is shorter than 6 characters", "Warning");
            }
            else
            {
                bool added = true;
                try
                {
                    added = await userServices.addUser(username, password, email, phoneNumber, name, surename);
                }
                catch(Exception)
                {
                    added = false;
                }
                if (added)
                {
                    mainWindow.Enabled = true;
                    this.Dispose();
                }
                else
                {
                    metroMessageBoxHelper.showWarning(this, "User adding unsuccesful", "Warning");
                }
            }
            Button_Create.Enabled = true;

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
