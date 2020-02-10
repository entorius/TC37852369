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
using TC37852369.Repository;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;

namespace TC37852369
{
    public partial class Login : MetroForm
    {
        public Login()
        {
            InitializeComponent();
            TextBoxModification TextBox_EmailMod = new TextBoxModification(TextBox_Email, "Username",false);
            TextBox_EmailMod.addEvents();
            TextBoxModification TextBox_PasswordMod = new TextBoxModification(TextBox_Password, "Password",true);
            TextBox_PasswordMod.addEvents();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }

        }
        
        private /*async*/ void Button_Login_Click(object sender, EventArgs e)
        {
            /*UserServices userServices = new UserServices();
            User user = await userServices.GetUser(TextBox_Email.Text, TextBox_Password.Text);
            if (user.id == null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Not correct email or password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {*/
                this.Hide();
                MainWindow mainWindow = new MainWindow(this);
                mainWindow.Show();
            /*}*/
        }

    }
}
