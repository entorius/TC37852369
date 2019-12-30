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
    public partial class GenerateTicket : MetroForm
    {
        MainWindow mainWindow;
        public GenerateTicket(MainWindow window)
        {
            mainWindow = window;
            this.FormClosed += CloseHandler;
            InitializeComponent();
            TextBoxModification TextBox_UserIdMod = new TextBoxModification(TextBox_UserId, "User Id", false);
            TextBox_UserIdMod.addEvents();
        }
        private void Button_Generate_Click(object sender, EventArgs e)
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
