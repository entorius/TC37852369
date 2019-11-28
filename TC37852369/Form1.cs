using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using TC37852369.Database;

namespace TC37852369
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "mtmfz13yNJJLuio1yYMKH1jbSQRIV5anaTXv5C4O",
            BasePath = "https://ticketbase-36d66.firebaseio.com/"
        };
        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                MessageBox.Show("connection established");
            }
        }

        private async void Button_AddUser_Click(object sender, EventArgs e)
        {
            DatabaseRequests request = new DatabaseRequests();
            bool success = await request.addUser(TextBox_UserID.Text,TextBox_UserName.Text,TextBox_password.Text,
                TextBox_mail.Text,TextBox_Name.Text,TextBox_UserID.Text);
        }

        private async void Button_DeleteUser_Click(object sender, EventArgs e)
        {
            DatabaseRequests request = new DatabaseRequests();
            bool success = await request.deleteUser(TextBox_UserID.Text);
        }
    }
}
