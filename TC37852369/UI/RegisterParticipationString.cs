﻿using MetroFramework.Forms;
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
using TC37852369.Services;
using TC37852369.UI.helpers;

namespace TC37852369
{
    public partial class RegisterParticipationString : MetroForm
    {
        RegisterParticipant registerParticipant;
        EditParticipant editParticipant;
        string participationForm;
        ParticipationFormatServices participationFormatServices = new ParticipationFormatServices();
        MetroMessageBoxHelper MetroMessageBoxHelper = new MetroMessageBoxHelper(); 
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

        private async void Button_Add_Click(object sender, EventArgs e)
        {
            ParticipationFormat participationFormat = await participationFormatServices.addParticipationFormat(TextBox_ParticipationFormatName.Text);
            if (participationForm.Equals("register"))
            {
                if (participationFormat != null)
                {
                    registerParticipant.participationFormats.Add(participationFormat);
                    registerParticipant.Enabled = true;
                }
                else
                {
                    MetroMessageBoxHelper.showWarning(this, "participation format add unsuccesful. There " +
                        "might be problems with database or your internet connection", "Warning");
                }
                
            }
            else if(participationForm.Equals("edit"))
            {
                if (participationFormat != null)
                {
                    editParticipant.participationFormats.Add(participationFormat);
                    editParticipant.Enabled = true;
                }
                else
                {
                    MetroMessageBoxHelper.showWarning(this, "participation format add unsuccesful. There " +
                        "might be problems with database or your internet connection", "Warning");
                }
            }
            this.Dispose();
            
        }
    }
}
