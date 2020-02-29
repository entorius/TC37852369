using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class Event
    {
        public long id { get; set; }
        public string eventName { get; set; }
        public DateTime date_From { get; set; }
        public int eventLengthDays { get; set; }
        public DateTime day1Date { get; set; }
        public DateTime day2Date { get; set; }
        public DateTime day3Date { get; set; }
        public DateTime day4Date { get; set; }
        public string day1TimeFrom { get; set; }
        public string day1TimeTo { get; set; }
        public string day2TimeFrom { get; set; }
        public string day2TimeTo { get; set; }
        public string day3TimeFrom { get; set; }
        public string day3TimeTo { get; set; }
        public string day4TimeFrom { get; set; }
        public string day4TimeTo { get; set; }
        public string webPage { get; set; }
        public string venueName { get; set; }
        public string venueAdress { get; set; }
        public string eventStatus { get; set; }
        public string comment { get; set; }
        public bool useTemplate { get; set; }
        public string current_Mail_Template { get; set; }
        public string emailBody { get; set; }
        public string emailSubject { get; set; }
        public string image1Link { get; set; }
        public string image2Link { get; set; }
        public string image3Link { get; set; }
        public string image4Link { get; set; }
        public string image5Link { get; set; }
        public Event(string id, string eventName, DateTime date_From,
            int eventLengthDays, DateTime day1Date, DateTime day2Date, DateTime day3Date,
            DateTime day4Date, string day1TimeFrom, string day1TimeTo,
            string day2TimeFrom, string day2TimeTo, string day3TimeFrom, string day3TimeTo,
            string day4TimeFrom, string day4TimeTo,string webPage,
            string venueName, string venueAdress, string eventStatus, string comment,
            bool useTemplate, string current_Mail_Template, string emailBody, string emailSubject)
        {
            long idToParse;
            long.TryParse(id,out idToParse);
            this.id = idToParse;
            this.eventName = eventName;
            this.date_From = date_From;
            this.eventLengthDays = eventLengthDays;
            this.day1Date = day1Date;
            this.day2Date= day2Date;
            this.day3Date= day3Date;
            this.day4Date= day4Date;
            this.day1TimeFrom= day1TimeFrom;
            this.day1TimeTo= day1TimeTo;
            this.day2TimeFrom= day2TimeFrom;
            this.day2TimeTo= day2TimeTo;
            this.day3TimeFrom= day3TimeFrom;
            this.day3TimeTo= day3TimeTo;
            this.day4TimeFrom= day4TimeFrom;
            this.day4TimeTo= day4TimeTo;
            this.webPage = webPage;
            this.venueName= venueName;
            this.venueAdress= venueAdress;
            this.eventStatus= eventStatus;
            this.comment= comment;
            this.useTemplate= useTemplate;
            if(current_Mail_Template.Length == 0)
            {
                current_Mail_Template = "Local email template";
            }
            this.current_Mail_Template= current_Mail_Template;
            this.emailSubject = emailSubject;
            this.emailBody= emailBody;
        }
    }
}
