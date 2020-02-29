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
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.UI.helpers;

namespace TC37852369.UI
{
    public partial class DeleteParticipantionFormat : MetroForm
    {
        RegisterParticipant registerParticipant;
        EditParticipant editParticipant;
        List<ParticipationFormat> participationFormats = new List<ParticipationFormat>();
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
        public DeleteParticipantionFormat(RegisterParticipant metroForm, List<ParticipationFormat> participationFormats)
        {
            InitializeComponent();
            registerParticipant = metroForm;
            this.participationFormats = participationFormats;
            this.FormClosed += CloseHandler;
            fillCombobox();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            BringToFront();
        }

        private void fillCombobox()
        {
            foreach (ParticipationFormat participationFormat in participationFormats) {
                this.ComboBox_ParticipationFormats.Items.Add(participationFormat.Value);
            }
        }

        public DeleteParticipantionFormat(EditParticipant metroForm, List<ParticipationFormat> participationFormats)
        {
            InitializeComponent();
            this.participationFormats = participationFormats;
            editParticipant = metroForm;
            fillCombobox();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            if(registerParticipant != null)
            {
                registerParticipant.Enabled = true;
            }
            else if (editParticipant != null)
            {
                editParticipant.Enabled = true;
            }
            this.Dispose();
        }
        protected void CloseHandler(object sender, EventArgs e)
        {
            if (registerParticipant != null)
            {
                registerParticipant.Enabled = true;
            }
            else if (editParticipant != null)
            {
                editParticipant.Enabled = true;
            }
        }

        private async void Button_Delete_Click(object sender, EventArgs e)
        {
            bool deleted = false;
            if (ComboBox_ParticipationFormats.SelectedIndex != 1)
            {
                if (registerParticipant != null)
                {
                    int index = registerParticipant.participationFormats.FindIndex(p => this.participationFormats[ComboBox_ParticipationFormats.SelectedIndex].Value.Equals(p.Value));
                     deleted = await participationFormatServices.deleteParticipationFormat(this.participationFormats[ComboBox_ParticipationFormats.SelectedIndex].Id);
                    if (deleted)
                    {
                        registerParticipant.participationFormats.RemoveAt(index);
                        registerParticipant.Enabled = true;
                    }
                }
                else if (editParticipant != null)
                {
                    int index = editParticipant.participationFormats.FindIndex(p => this.participationFormats[ComboBox_ParticipationFormats.SelectedIndex].Value.Equals(p.Value));
                     deleted = await participationFormatServices.deleteParticipationFormat(this.participationFormats[ComboBox_ParticipationFormats.SelectedIndex].Id);
                    if (deleted)
                    {
                        editParticipant.participationFormats.RemoveAt(index);
                        editParticipant.Enabled = true;
                    }
                }
                if (deleted)
                {
                    this.Dispose();
                }
                else
                {
                    messageBoxHelper.showWarning(this, "Delete unsuccesful\n" +
                        "1) Might be problems with internet connection\n" +
                        "2) Might be problems with database. If problem continues contact support", "Warning");
                }
            }
            else
            {
                messageBoxHelper.showWarning(this, "No participation format was choosen", "Warning");
            }
        }
    }
}
