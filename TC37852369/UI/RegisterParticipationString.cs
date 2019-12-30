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
        public RegisterParticipationString(RegisterParticipant registerParticipant)
        {
            this.registerParticipant = registerParticipant;
            InitializeComponent();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            registerParticipant.Enabled = true;
            this.Dispose();
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            registerParticipant.participationFormats.Add(TextBox_ParticipationFormatName.Text);
            registerParticipant.Enabled = true;
            this.Dispose();
            
        }
    }
}
