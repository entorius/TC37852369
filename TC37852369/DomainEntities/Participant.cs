using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class Participant
    {
        public string participantId { get; set; }
        public string eventId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string jobTitle { get; set; }
        public string companyName { get; set; }
        public string companyType { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string country { get; set; }
        public string participationFormat { get; set; }
        public string paymentStatus { get; set; }
        public string ticketBarcode { get; set; }
        public bool ticketSent { get; set; }
        public bool materials { get; set; }
        public bool participateEveningEvent { get; set; }

        public bool participateInDay1 { get; set; }
        public bool participateInDay2 { get; set; }
        public bool participateInDay3 { get; set; }
        public bool participateInDay4 { get; set; }

        public bool checkedInDay1 { get; set; }
        public bool checkedInDay2 { get; set; }
        public bool checkedInDay3 { get; set; }
        public bool checkedInDay4 { get; set; }

        public Participant(string participant_Id, string event_Id,
            string firstName, string lastName,string jobTitle, string company_Name, string company_Type,
            string email, string phone_Number, string country, string participation_Format,
            string payment_Status, bool materials, string ticket_Bar, bool ticket_Sent,
            bool participate_Evening_Event, bool participate_In_Day1, bool participate_In_Day2,
            bool participate_In_Day3, bool participate_In_Day4, bool checked_In_Day1,
            bool checked_In_Day2, bool checked_In_Day3, bool checked_In_Day4)
        {
            this.participantId = participant_Id;
            this.eventId = event_Id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.jobTitle = jobTitle;
            this.companyName = company_Name;
            this.companyType = company_Type;
            this.email = email;
            this.phoneNumber = phone_Number;
            this.country = country;
            this.participationFormat = participation_Format;
            this.paymentStatus = payment_Status;
            this.materials = materials;
            this.ticketBarcode = ticket_Bar;
            this.ticketSent = ticket_Sent;
            this.participateEveningEvent = participate_Evening_Event;
            this.participateInDay1 = participate_In_Day1;
            this.participateInDay2 = participate_In_Day2;
            this.participateInDay3 = participate_In_Day3;
            this.participateInDay4 = participate_In_Day4;
            this.checkedInDay1 = checked_In_Day1;
            this.checkedInDay2 = checked_In_Day2;
            this.checkedInDay3 = checked_In_Day3;
            this.checkedInDay4 = checked_In_Day4;

        }
    }
}
