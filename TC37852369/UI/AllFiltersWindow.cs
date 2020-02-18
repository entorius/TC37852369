using MetroFramework.Controls;
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
    public partial class AllFiltersWindow : MetroForm
    {
        List<MetroCheckBox> allActivateFilter = new List<MetroCheckBox>();
        List<Participant> participants;
        ParticipantFilteringServices pfs = new ParticipantFilteringServices();
        Event eventEntity;
        MainWindow mainWindow;
        FilterWindowData filterWindowData;
        MetroMessageBoxHelper messageHelper = new MetroMessageBoxHelper();
        public AllFiltersWindow(MainWindow mainWindow,Event eventEntity, FilterWindowData filterWindowData,
            List<ParticipationFormat> participationFormats, List<Participant> participants)
        {
            this.filterWindowData = filterWindowData;
            this.eventEntity = eventEntity;
            this.participants = participants;
            this.mainWindow = mainWindow;
            InitializeComponent();

            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            this.FormClosed += ClosedHandler;
            allActivateFilter.Add(CheckBox_FirstName);
            allActivateFilter.Add(CheckBox_LastName);
            allActivateFilter.Add(CheckBox_CompanyType);
            allActivateFilter.Add(CheckBox_JobTitle);
            allActivateFilter.Add(CheckBox_CompanyName);
            allActivateFilter.Add(CheckBox_PaymentStatus);
            allActivateFilter.Add(CheckBox_ParticipationFormat);
            allActivateFilter.Add(CheckBox_TicketSent);
            allActivateFilter.Add(CheckBox_Materials);
            allActivateFilter.Add(CheckBox_RegisteredInDay);
            allActivateFilter.Add(CheckBox_CheckedInDay);
            allActivateFilter.Add(CheckBox_Country);

            foreach (CompanyTypes companyType in (CompanyTypes[])Enum.GetValues(typeof(CompanyTypes)))
            {
                ComboBox_CompanyType.Items.Add(companyType.ToString());
            }


            foreach (ParticipationFormat participationFormat in participationFormats)
            {
                ComboBox_ParticipationFormat.Items.Add(participationFormat.Value);
            }

            //Set payment status values to comboBox and select first item
            foreach (PaymentStatus paymentStatus in (PaymentStatus[])Enum.GetValues(typeof(PaymentStatus)))
            {
                ComboBox_PaymentStatus.Items.Add(paymentStatus.ToString());
            }
            List<string> YesNoList = new List<string>();
            foreach (YesNo yesNo in (YesNo[])Enum.GetValues(typeof(YesNo)))
            {
                ComboBox_TicketSent.Items.Add(yesNo.ToString());
                ComboBox_RegisteredInDay.Items.Add(yesNo.ToString());
                ComboBox_CheckedInDay.Items.Add(yesNo.ToString());
            }
            LoadWindowData();
        }
        private void LoadWindowData()
        {
            TextBox_FirstName.Text = filterWindowData.firstName;
            TextBox_LastName.Text = filterWindowData.lastName;
            ComboBox_CompanyType.SelectedIndex = ComboBox_CompanyType.Items.IndexOf(filterWindowData.companyType);
            TextBox_JobTitle.Text = filterWindowData.jobTitle;
            TextBox_CompanyName.Text = filterWindowData.companyName;
            ComboBox_PaymentStatus.SelectedIndex = ComboBox_PaymentStatus.Items.IndexOf(filterWindowData.paymentStatus);
            ComboBox_ParticipationFormat.SelectedIndex = ComboBox_ParticipationFormat.Items.IndexOf(filterWindowData.participationFormat);
            ComboBox_TicketSent.SelectedIndex = ComboBox_TicketSent.Items.IndexOf(filterWindowData.ticketSent);
            ComboBox_Materials.SelectedIndex = ComboBox_Materials.Items.IndexOf(filterWindowData.materials);
            ComboBox_RegisteredInDay.SelectedIndex = ComboBox_RegisteredInDay.Items.IndexOf(filterWindowData.registeredInDay);
            ComboBox_CheckedInDay.SelectedIndex = ComboBox_CheckedInDay.Items.IndexOf(filterWindowData.checkedInDay);
            TextBox_Country.Text = filterWindowData.country;

            CheckBox_FirstName.Checked = filterWindowData.firstNameActive;
            CheckBox_LastName.Checked = filterWindowData.lastNameActive;
            CheckBox_CompanyType.Checked = filterWindowData.companyTypeActive;
            CheckBox_JobTitle.Checked = filterWindowData.jobTitleActive;
            CheckBox_CompanyName.Checked = filterWindowData.companyNameActive;
            CheckBox_PaymentStatus.Checked = filterWindowData.paymentStatusActive;
            CheckBox_ParticipationFormat.Checked = filterWindowData.participationFormatActive;
            CheckBox_TicketSent.Checked = filterWindowData.ticketSentActive;
            CheckBox_Materials.Checked = filterWindowData.materialsActive;
            CheckBox_RegisteredInDay.Checked = filterWindowData.registeredInDayActive;
            CheckBox_CheckedInDay.Checked = filterWindowData.checkedInDayActive;
            CheckBox_Country.Checked = filterWindowData.countryActive;

            CheckBox_FirstNameAscending.Checked = filterWindowData.firstNameAscendingChecked;
            CheckBox_FirstNameDescending.Checked = filterWindowData.firstNameDescendingChecked;
            CheckBox_LastNameAscending.Checked = filterWindowData.lastNameAscendingChecked;
            CheckBox_LastNameDescending.Checked = filterWindowData.lastNameDescendingChecked;
            CheckBox_JobTitleAscending.Checked = filterWindowData.jobTitleAscendingChecked;
            CheckBox_JobTitleDescending.Checked = filterWindowData.jobTitleDescendingChecked;
            CheckBox_CompanyNameAscending.Checked = filterWindowData.companyNameAscendingChecked;
            CheckBox_CompanyNameDescending.Checked = filterWindowData.companyNameDescendingChecked;
            CheckBox_CountryAscending.Checked = filterWindowData.countryAscendingChecked;
            CheckBox_CountryDescending.Checked = filterWindowData.countryDescendingChecked;


        }

        private void CheckBox_FirstNameAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_FirstNameDescending.Checked)
            {
                CheckBox_FirstNameDescending.Checked = false;
            }
        }

        private void CheckBox_FirstNameDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_FirstNameAscending.Checked)
            {
                CheckBox_FirstNameAscending.Checked = false;
            }
        }

        private void CheckBox_LastNameAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_LastNameDescending.Checked)
            {
                CheckBox_LastNameDescending.Checked = false;
            }
        }

        private void CheckBox_LastNameDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_LastNameAscending.Checked)
            {
                CheckBox_LastNameAscending.Checked = false;
            }
        }

        private void CheckBox_JobTitleAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_JobTitleDescending.Checked)
            {
                CheckBox_JobTitleDescending.Checked = false;
            }
        }

        private void CheckBox_JobTitleDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_JobTitleAscending.Checked)
            {
                CheckBox_JobTitleAscending.Checked = false;
            }
        }

        private void CheckBox_CompanyNameAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_CompanyNameDescending.Checked)
            {
                CheckBox_CompanyNameDescending.Checked = false;
            }
        }

        private void CheckBox_CompanyNameDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_CompanyNameAscending.Checked)
            {
                CheckBox_CompanyNameAscending.Checked = false;
            }
        }

        private void CheckBox_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_CheckAll.Checked)
            {
                foreach(MetroCheckBox ch in allActivateFilter)
                {
                    ch.Checked = true;
                }
            }
        }

        private void Button_Filter_Click(object sender, EventArgs e)
        {
            Button_Filter.Enabled = false;
            bool dateNotCorrect = false;
            List<Participant> filteredParticipants = participants;
            if (CheckBox_FirstName.Checked)
            {
                if (TextBox_FirstName.Text.Replace(" ", "").Length > 0)
                {
                    filteredParticipants = pfs.filterAccordingToFirstName(filteredParticipants, TextBox_FirstName.Text);
                }
            }
            if (CheckBox_LastName.Checked)
            {
                if (TextBox_LastName.Text.Replace(" ", "").Length > 0)
                {
                    filteredParticipants = pfs.filterAccordingToLastName(filteredParticipants, TextBox_LastName.Text);
                }
            }
            if (CheckBox_CompanyType.Checked)
            {
                if (ComboBox_CompanyType.SelectedIndex >= 0)
                {
                    filteredParticipants = pfs.filterAccordingToCompanyType(filteredParticipants, ComboBox_CompanyType.SelectedItem.ToString());
                }
            }
            if (CheckBox_JobTitle.Checked)
            {
                if (TextBox_JobTitle.Text.Replace(" ", "").Length > 0)
                {
                    filteredParticipants = pfs.filterAccordingToJobTitle(filteredParticipants, TextBox_JobTitle.Text);
                }
            }
            if (CheckBox_CompanyName.Checked)
            {
                if (TextBox_CompanyName.Text.Replace(" ", "").Length > 0)
                {
                    filteredParticipants = pfs.filterAccordingToCompanyName(filteredParticipants, TextBox_CompanyName.Text);
                }
            }
            if (CheckBox_PaymentStatus.Checked)
            {
                if (ComboBox_PaymentStatus.SelectedIndex >= 0)
                {
                    filteredParticipants = pfs.filterAccordingToPaymentStatus(filteredParticipants, ComboBox_PaymentStatus.SelectedItem.ToString());
                }
            }
            if (CheckBox_ParticipationFormat.Checked)
            {
                if (ComboBox_ParticipationFormat.SelectedIndex >= 0)
                {
                    filteredParticipants = pfs.filterAccordingToParticipationFormat(filteredParticipants, ComboBox_ParticipationFormat.SelectedItem.ToString());
                }
            }
            if (CheckBox_TicketSent.Checked)
            {
                if (ComboBox_TicketSent.SelectedIndex >= 0)
                {
                    filteredParticipants = pfs.filterAccordingToTicketSent(filteredParticipants, ComboBox_TicketSent.SelectedItem.ToString().Equals("Yes"));
                }
            }
            if (CheckBox_Materials.Checked)
            {
                if (ComboBox_Materials.SelectedIndex >= 0)
                {
                    filteredParticipants = pfs.filterAccordingToMaterials(filteredParticipants, ComboBox_Materials.SelectedItem.ToString());
                }
            }
            if (CheckBox_RegistrationDate.Checked)
            {
                int year;
                int month;
                int day;
                bool yearParsed = TextBox_RegistrationDateYear.Text.Length > 0 ? Int32.TryParse(TextBox_RegistrationDateYear.Text, out year) : true;
                bool monthParsed = TextBox_RegistrationDateMonth.Text.Length > 0 ? Int32.TryParse(TextBox_RegistrationDateMonth.Text, out month) : true;
                bool dayParsed = TextBox_RegistrationDateDay.Text.Length > 0 ? Int32.TryParse(TextBox_RegistrationDateDay.Text, out day) : true;


                if (yearParsed && monthParsed && dayParsed)
                {
                    filteredParticipants = pfs.filterAccordingToRegistrationDate(filteredParticipants, TextBox_RegistrationDateYear.Text,
                        TextBox_RegistrationDateMonth.Text, TextBox_RegistrationDateDay.Text);
                }
                else
                {
                    dateNotCorrect = true;
                }
            }
            if (CheckBox_PaymentDate.Checked)
            {
                int year;
                int month;
                int day;
                bool yearParsed = TextBox_PaymentDateYear.Text.Length > 0 ? Int32.TryParse(TextBox_PaymentDateYear.Text,out year):true;
                bool monthParsed = TextBox_PaymentDateMonth.Text.Length > 0 ? Int32.TryParse(TextBox_PaymentDateMonth.Text, out month): true;
                bool dayParsed = TextBox_PaymentDateDay.Text.Length > 0 ? Int32.TryParse(TextBox_PaymentDateDay.Text, out day):true;


                if (yearParsed && monthParsed && dayParsed)
                {
                    filteredParticipants = pfs.filterAccordingToPaymentDate(filteredParticipants, TextBox_PaymentDateYear.Text,
                        TextBox_PaymentDateMonth.Text, TextBox_PaymentDateDay.Text);
                }
                else
                {
                    dateNotCorrect = true;
                }
            }
            if (CheckBox_RegisteredInDay.Checked)
            {
                if (ComboBox_Materials.SelectedIndex >= 0)
                {
                    List<Participant> part = pfs.filterAccordingToRegisteredInDay(eventEntity,filteredParticipants, ComboBox_RegisteredInDay.SelectedItem.ToString().Equals("Yes"));
                    if(part != null)
                    {
                        filteredParticipants = part;
                    }               
                }
            }
            if (CheckBox_CheckedInDay.Checked)
            {
                List<Participant> part = pfs.filterAccordingToCheckedInDay(eventEntity, filteredParticipants, ComboBox_CheckedInDay.SelectedItem.ToString().Equals("Yes"));
                if (part != null)
                {
                    filteredParticipants = part;
                }
            }
            if (CheckBox_Country.Checked)
            {
                if (TextBox_Country.Text.Replace(" ", "").Length > 0)
                {
                    filteredParticipants = pfs.filterAccordingToCountry(filteredParticipants, TextBox_Country.Text);
                }
            }
            if (!dateNotCorrect)
            {
                bool orderByFirstName = CheckBox_FirstName.Checked &&
                    (CheckBox_FirstNameAscending.Checked || CheckBox_FirstNameDescending.Checked);
                bool orderByLastName = CheckBox_LastName.Checked &&
                    (CheckBox_LastNameAscending.Checked || CheckBox_LastNameDescending.Checked);
                bool orderByJobTitle = CheckBox_JobTitle.Checked &&
                    (CheckBox_JobTitleAscending.Checked || CheckBox_JobTitleDescending.Checked);
                bool orderByCompanyName = CheckBox_CompanyName.Checked &&
                    (CheckBox_CompanyNameAscending.Checked || CheckBox_CompanyNameDescending.Checked);
                bool orderByCountry = CheckBox_Country.Checked &&
                   (CheckBox_CountryAscending.Checked || CheckBox_CountryDescending.Checked);
                bool orderByRegistrationDate = CheckBox_RegistrationDate.Checked &&
                   (CheckBox_RegistrationDateAscending.Checked || CheckBox_RegistrationDateDescending.Checked);
                bool orderByPaymentDate = CheckBox_PaymentDate.Checked &&
                   (CheckBox_PaymentDateAscending.Checked || CheckBox_PaymentDateDescending.Checked);
                filteredParticipants = pfs.SortParticipantsAscendingDescending(filteredParticipants, orderByFirstName, orderByLastName,
                    orderByJobTitle, orderByCompanyName, orderByCountry, orderByRegistrationDate, orderByPaymentDate,
                    CheckBox_FirstNameAscending.Checked, CheckBox_LastNameAscending.Checked,
                    CheckBox_JobTitleAscending.Checked, CheckBox_CompanyNameAscending.Checked, CheckBox_CountryAscending.Checked,
                    CheckBox_RegistrationDateAscending.Checked, CheckBox_RegistrationDateDescending.Checked,
                    CheckBox_PaymentDateAscending.Checked, CheckBox_PaymentDateDescending.Checked);

                mainWindow.filteredParticipants = filteredParticipants;
                mainWindow.emptyTable(mainWindow.Table_ParticipantsData1);
                mainWindow.UpdateParticipantsTable(filteredParticipants);
                updateFiltersData();
                mainWindow.filterWindowData = filterWindowData;
                mainWindow.TextBox_FirstNameFilter.Text = filterWindowData.firstName;
                mainWindow.TextBox_LastNameFilter.Text = filterWindowData.lastName;
                mainWindow.TextBox_CompanyNameFilter.Text = filterWindowData.companyName;
                mainWindow.ComboBox_PaymentStatus.SelectedIndex =
                    mainWindow.ComboBox_PaymentStatus.Items.IndexOf(filterWindowData.paymentStatus);
                Button_Filter.Enabled = true;
                mainWindow.Enabled = true;
                this.Dispose();
            }
            else
            {
                messageHelper.showWarning(this, "Registration date or payment date was entered incorectly (can only contain numbers)", "Warning");
            }
        }
        private void updateFiltersData()
        {
            filterWindowData.firstName = TextBox_FirstName.Text;
            filterWindowData.lastName = TextBox_LastName.Text;
            filterWindowData.companyType = ComboBox_CompanyType.SelectedItem.ToString();
            filterWindowData.jobTitle = TextBox_JobTitle.Text;
            filterWindowData.companyName = TextBox_CompanyName.Text;
            filterWindowData.paymentStatus = ComboBox_PaymentStatus.SelectedItem.ToString();
            filterWindowData.RegistrationDateYear = TextBox_RegistrationDateYear.Text;
            filterWindowData.RegistrationDateMonth = TextBox_RegistrationDateMonth.Text;
            filterWindowData.RegistrationDateDay = TextBox_RegistrationDateDay.Text;
            filterWindowData.PaymentDateYear = TextBox_PaymentDateYear.Text;
            filterWindowData.PaymentDateMonth = TextBox_PaymentDateMonth.Text;
            filterWindowData.PaymentDateDay = TextBox_PaymentDateDay.Text;
            filterWindowData.participationFormat = ComboBox_ParticipationFormat.SelectedItem.ToString();
            filterWindowData.ticketSent = ComboBox_TicketSent.SelectedIndex == 0;
            filterWindowData.materials = ComboBox_Materials.SelectedValue.ToString();
            filterWindowData.registeredInDay = ComboBox_RegisteredInDay.SelectedIndex == 0;
            filterWindowData.checkedInDay = ComboBox_CheckedInDay.SelectedIndex == 0;
            filterWindowData.country = TextBox_Country.Text;

            filterWindowData.firstNameActive = CheckBox_FirstName.Checked;
            filterWindowData.lastNameActive = CheckBox_LastName.Checked;
            filterWindowData.companyTypeActive = CheckBox_CompanyType.Checked;
            filterWindowData.jobTitleActive = CheckBox_JobTitle.Checked;
            filterWindowData.companyNameActive = CheckBox_CompanyName.Checked;
            filterWindowData.paymentStatusActive = CheckBox_PaymentStatus.Checked;
            filterWindowData.registrationDateActive = CheckBox_RegistrationDate.Checked;
            filterWindowData.paymentDateActive = CheckBox_PaymentDate.Checked;
            filterWindowData.participationFormatActive = CheckBox_ParticipationFormat.Checked;
            filterWindowData.ticketSentActive = CheckBox_TicketSent.Checked;
            filterWindowData.materialsActive = CheckBox_Materials.Checked;
            filterWindowData.registeredInDayActive = CheckBox_RegisteredInDay.Checked;
            filterWindowData.checkedInDayActive = CheckBox_CheckedInDay.Checked;
            filterWindowData.countryActive = CheckBox_Country.Checked;

            filterWindowData.firstNameAscendingChecked = CheckBox_FirstNameAscending.Checked;
            filterWindowData.firstNameDescendingChecked = CheckBox_FirstNameDescending.Checked;
            filterWindowData.lastNameAscendingChecked = CheckBox_LastNameAscending.Checked;
            filterWindowData.lastNameDescendingChecked = CheckBox_LastNameDescending.Checked;
            filterWindowData.jobTitleAscendingChecked = CheckBox_JobTitleAscending.Checked;
            filterWindowData.jobTitleDescendingChecked = CheckBox_JobTitleDescending.Checked;
            filterWindowData.companyNameAscendingChecked = CheckBox_CompanyNameAscending.Checked;
            filterWindowData.companyNameDescendingChecked = CheckBox_CompanyNameDescending.Checked;
            filterWindowData.countryAscendingChecked = CheckBox_CountryAscending.Checked;
            filterWindowData.countryDescendingChecked = CheckBox_CountryDescending.Checked;
            filterWindowData.RegistrationDateAscendingChecked = CheckBox_RegistrationDateAscending.Checked;
            filterWindowData.RegistrationDateDescendingChecked = CheckBox_RegistrationDateDescending.Checked;
            filterWindowData.PaymentDateAscendingChecked = CheckBox_RegistrationDateAscending.Checked;
            filterWindowData.PaymentDateDescendingChecked = CheckBox_RegistrationDateDescending.Checked;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }
        private void ClosedHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }
    }
}
