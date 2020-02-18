using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class FilterWindowData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyType { get; set; }
        public string jobTitle { get; set; }
        public string companyName { get; set; }
        public string paymentStatus { get; set; }
        public string RegistrationDateYear { get; set; }
        public string RegistrationDateMonth { get; set; }
        public string RegistrationDateDay { get; set; }
        public string PaymentDateYear { get; set; }
        public string PaymentDateMonth { get; set; }
        public string PaymentDateDay { get; set; }
        public string participationFormat { get; set; }
        public bool ticketSent { get; set; }
        public string materials { get; set; }
        public bool registeredInDay { get; set; }
        public bool checkedInDay { get; set; }
        public string country { get; set; }
        public bool firstNameAscendingChecked { get; set; }
        public bool firstNameDescendingChecked { get; set; }
        public bool lastNameAscendingChecked { get; set; }
        public bool lastNameDescendingChecked { get; set; }
        public bool jobTitleAscendingChecked { get; set; }
        public bool jobTitleDescendingChecked { get; set; }
        public bool companyNameAscendingChecked { get; set; }
        public bool companyNameDescendingChecked { get; set; }
        public bool countryAscendingChecked { get; set; }
        public bool countryDescendingChecked { get; set; }
        public bool RegistrationDateAscendingChecked { get; set; }
        public bool RegistrationDateDescendingChecked { get; set; }
        public bool PaymentDateAscendingChecked { get; set; }
        public bool PaymentDateDescendingChecked { get; set; }
        public bool firstNameActive { get; set; }
        public bool lastNameActive { get; set; }
        public bool companyTypeActive { get; set; }
        public bool jobTitleActive { get; set; }
        public bool companyNameActive { get; set; }
        public bool paymentStatusActive { get; set; }
        public bool participationFormatActive { get; set; }
        public bool ticketSentActive { get; set; }
        public bool materialsActive { get; set; }
        public bool registeredInDayActive { get; set; }
        public bool checkedInDayActive { get; set; }
        public bool countryActive { get; set; }
        public bool registrationDateActive { get; set; }
        public bool paymentDateActive { get; set; }
        public FilterWindowData(string firstName,string lastName, string companyType,string jobTitle,string companyName,string paymentStatus,
        string RegistrationDateYear, string RegistrationDateMonth, string RegistrationDateDay, string PaymentDateYear, string PaymentDateMonth,
        string PaymentDateDay, string participationFormat,bool ticketSent,string materials,bool registeredInDay,bool checkedInDay,string country,
        bool firstNameAscendingChecked ,bool firstNameDescendingChecked, bool lastNameAscendingChecked, bool lastNameDescendingChecked,
        bool jobTitleAscendingChecked, bool jobTitleDescendingChecked, bool companyNameAscendingChecked, bool companyNameDescendingChecked,
        bool countryAscendingChecked, bool countryDescendingChecked, bool RegistrationDateAscendingChecked, bool RegistrationDateDescendingChecked,
        bool PaymentDateAscendingChecked, bool PaymentDateDescendingChecked, bool firstNameActive, bool lastNameActive, bool companyTypeActive, 
        bool jobTitleActive, bool companyNameActive, bool paymentStatusActive,  bool participationFormatActive, bool ticketSentActive, 
        bool materialsActive, bool registeredInDayActive, bool checkedInDayActive, bool countryActive, bool registrationDateActive,
        bool paymentDateActice)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.companyType = companyType;
            this.jobTitle = jobTitle;
            this.companyName = companyName;
            this.paymentStatus = paymentStatus;
            this.RegistrationDateYear = RegistrationDateYear;
            this.RegistrationDateMonth = RegistrationDateMonth;
            this.RegistrationDateDay = RegistrationDateDay;
            this.PaymentDateYear = PaymentDateYear;
            this.PaymentDateMonth = PaymentDateMonth;
            this.PaymentDateDay = PaymentDateDay;
            this.participationFormat = participationFormat;
            this.ticketSent = ticketSent;
            this.materials = materials;
            this.registeredInDay = registeredInDay;
            this.checkedInDay = checkedInDay;
            this.country = country;
            this.firstNameAscendingChecked = firstNameAscendingChecked;
            this.firstNameDescendingChecked = firstNameDescendingChecked;
            this.lastNameAscendingChecked = lastNameAscendingChecked;
            this.lastNameDescendingChecked = lastNameDescendingChecked;
            this.jobTitleAscendingChecked = jobTitleAscendingChecked;
            this.jobTitleDescendingChecked = jobTitleDescendingChecked;
            this.companyNameAscendingChecked = companyNameAscendingChecked;
            this.companyNameDescendingChecked = companyNameDescendingChecked;
            this.countryAscendingChecked = countryAscendingChecked;
            this.countryDescendingChecked = countryDescendingChecked;
            this.RegistrationDateAscendingChecked = RegistrationDateAscendingChecked;
            this.RegistrationDateDescendingChecked = RegistrationDateDescendingChecked;
            this.PaymentDateAscendingChecked = PaymentDateAscendingChecked;
            this.PaymentDateDescendingChecked = PaymentDateDescendingChecked;
            this.firstNameActive = firstNameActive;
            this.lastNameActive = lastNameActive;
            this.companyTypeActive = companyTypeActive;
            this.companyNameActive = companyNameActive;
            this.jobTitleActive = jobTitleActive;
            this.companyNameActive = companyNameActive;
            this.paymentStatusActive = paymentStatusActive;
            this.participationFormatActive = participationFormatActive;
            this.ticketSentActive = ticketSentActive;
            this.materialsActive = materialsActive;
            this.registeredInDayActive = registeredInDayActive;
            this.checkedInDayActive = checkedInDayActive;
            this.countryActive = countryActive;
            this.registrationDateActive = registrationDateActive;
            this.paymentDateActive = paymentDateActice;
           

        }
    }
}
