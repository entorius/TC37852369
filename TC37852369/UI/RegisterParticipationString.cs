using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369
{
    public partial class RegisterParticipationString : MetroForm
    {
        RegisterParticipant registerParticipant;
        EditParticipant editParticipant;
        string participationForm;
        public RegisterParticipationString(RegisterParticipant registerParticipant)
        {
            this.registerParticipant = registerParticipant;
            participationForm = "register";
            InitializeComponent();
        }
        public RegisterParticipationString(EditParticipant editParticipant)
        {
            this.editParticipant = editParticipant;
            participationForm = "edit";
            InitializeComponent();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            registerParticipant.Enabled = true;
            this.Dispose();
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (participationForm.Equals("register"))
            {
                registerParticipant.participationFormats.Add(TextBox_ParticipationFormatName.Text);
                registerParticipant.Enabled = true;
            }
            else if(participationForm.Equals("edit"))
            {
                editParticipant.participationFormats.Add(TextBox_ParticipationFormatName.Text);
                editParticipant.Enabled = true;
            }
            this.Dispose();
            
        }
    }
}
