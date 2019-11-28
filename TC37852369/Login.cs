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
    public partial class Login : MetroForm
    {
        public Login()
        {
            InitializeComponent();
            TextBoxModification TextBox_EmailMod = new TextBoxModification(TextBox_Email, "Email",false);
            TextBox_EmailMod.addEvents();
            TextBoxModification TextBox_PasswordMod = new TextBoxModification(TextBox_Password, "Password",true);
            TextBox_PasswordMod.addEvents();

        }
        
        private void Button_Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow(this);
            mainWindow.Show();
        }
    }
}
