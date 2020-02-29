using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

namespace TC37852369.Services.EmailSending
{
    public class EmailStringHelper
    {
        
        public string formatEmailString(string emailString, Participant participant, Event eventE)
        {
            double paymentAmount = participant.paymentAmount;
            emailString = replaceFirstName(emailString, participant.firstName);
            emailString = replaceLastName(emailString, participant.lastName);
            emailString = replaceCompanyName(emailString, participant.companyName);
            emailString = replacePaymentAmount(emailString, paymentAmount.ToString());
            emailString = replaceParticipationFormat(emailString, participant.participationFormat);
            return emailString;
        }
        private string replaceFirstName(string emailString,string firstName)
        {
            return emailString.Replace("%FirstName%", firstName);
        }
        private string replaceLastName(string emailString, string lastName)
        {
            return emailString.Replace("%LastName%", lastName);
        }
        private string replaceCompanyName(string emailString, string companyName)
        {
            return emailString.Replace("%CompanyName%", companyName);
        }

        private string replacePaymentAmount(string emailString, string paymentAmount)
        {
            return emailString.Replace("%PaymentAmount%", paymentAmount);
        }
        private string replaceParticipationFormat(string emailString, string participationFormat)
        {
            return emailString.Replace("%ParticipationFormat%", participationFormat);
        }
        private int CountParticipationDays(Participant participant)
        {
            int participationDays = 0;
            if (participant.participateInDay1)
            {
                participationDays = participationDays + 1;
            }
            if (participant.participateInDay2)
            {
                participationDays = participationDays + 1;
            }
            if (participant.participateInDay3)
            {
                participationDays = participationDays + 1;
            }
            if (participant.participateInDay4)
            {
                participationDays = participationDays + 1;
            }
            return participationDays;
        }
    }
}
