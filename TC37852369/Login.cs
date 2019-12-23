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
using TC37852369.Database;
using TC37852369.DatabaseEntities;
using TC37852369.Helpers;

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
        
        private /*async*/ void Button_Login_Click(object sender, EventArgs e)
        {
            //DatabaseRequests db = new DatabaseRequests();
            //User user = await db.GetUser(TextBox_Email.Text, TextBox_Password.Text); 
            //if (user.id == null)
            //{
                //MetroFramework.MetroMessageBox.Show(this, "Not correct email or password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //this.Hide();
            MainWindow mainWindow = new MainWindow(this);
            mainWindow.Show();
        }

    }
}
