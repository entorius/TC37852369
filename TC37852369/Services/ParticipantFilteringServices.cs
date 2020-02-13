using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Helpers;

namespace TC37852369.Services
{
    public class ParticipantFilteringServices
    {
        public List<Participant>filterAccordingToFirstName(List<Participant> participants, string firstName)
        {
            return participants.FindAll(p => p.firstName.Contains(firstName));
        }
        public List<Participant> filterAccordingToLastName(List<Participant> participants, string lastName)
        {
            return participants.FindAll(p => p.lastName.Contains(lastName));
        }
        public List<Participant> filterAccordingToCompanyType(List<Participant> participants, string companyType)
        {
            return participants.FindAll(p => p.companyType.Equals(companyType));
        }
        public List<Participant> filterAccordingToJobTitle(List<Participant> participants, string jobTitle)
        {
            return participants.FindAll(p => p.jobTitle.Contains(jobTitle));
        }
        public List<Participant> filterAccordingToCompanyName(List<Participant> participants, string companyName)
        {
            return participants.FindAll(p => p.companyName.Contains(companyName));
        }
        public List<Participant> filterAccordingToPaymentStatus(List<Participant> participants, string paymentStatus)
        {
            return participants.FindAll(p => p.paymentStatus.Equals(paymentStatus));
        }
        public List<Participant> filterAccordingToParticipationFormat(List<Participant> participants, string participationFormat)
        {
            return participants.FindAll(p => p.participationFormat.Equals(participationFormat));
        }
        public List<Participant> filterAccordingToTicketSent(List<Participant> participants, bool ticketSent)
        {
            return participants.FindAll(p => p.ticketSent == ticketSent);
        }
        public List<Participant> filterAccordingToMaterials(List<Participant> participants, string materials)
        {
            bool mat = materials.Equals("Yes") ? true : false;
            return participants.FindAll(p => p.materials == mat);
        }
        public List<Participant> filterAccordingToRegisteredInDay(Event eventEntity,List<Participant> participants, bool registeredInDay)
        {
            DateTime today = DateHelper.setDateToMidnight(DateTime.Now);
            if (today.Equals(eventEntity.date_From))
            {
                return participants.FindAll(p => p.participateInDay1 == registeredInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(1)))
            {
                return participants.FindAll(p => p.participateInDay2 == registeredInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(2)))
            {
                return participants.FindAll(p => p.participateInDay3 == registeredInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(3)))
            {
                return participants.FindAll(p => p.participateInDay4 == registeredInDay);
            }
            return null;
        }
        public List<Participant> filterAccordingToCheckedInDay(Event eventEntity, List<Participant> participants, bool checkedInDay)
        {
            DateTime today = DateHelper.setDateToMidnight(DateTime.Now);
            if (today.Equals(eventEntity.date_From))
            {
                return participants.FindAll(p => p.checkedInDay1 == checkedInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(1)))
            {
                return participants.FindAll(p => p.checkedInDay2 == checkedInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(2)))
            {
                return participants.FindAll(p => p.checkedInDay3 == checkedInDay);
            }
            else if (today.Equals(eventEntity.date_From.AddDays(3)))
            {
                return participants.FindAll(p => p.checkedInDay4 == checkedInDay);
            }
            return new List<Participant>();
        }
        public List<Participant> filterAccordingToCountry(List<Participant> participants, string country)
        {
            return participants.FindAll(p => p.country.Contains(country));
        }
        public List<Participant> SortParticipantsAscendingDescending(List<Participant> participants,bool orderByFirstName, 
            bool orderByLastName, bool orderByJobTitle, bool orderByCompanyName, bool orderByCountry, bool orderByFirstNameAscending,
            bool orderByLastNameAscending, bool orderByJobTitleAscending, bool orderByCompanyNameAscending, bool orderByCountryAscending)
        {
            List<Participant> sortedParticipants = participants;

            var orderedParticipants = sortedParticipants.OrderBy(p => 0);

            if (orderByFirstName)
            {
                if (orderByFirstNameAscending)
                {
                    orderedParticipants.ThenBy(p => p.firstName);
                }
                else
                {
                    orderedParticipants.ThenByDescending(p => p.firstName);
                }
            }
            if (orderByLastName)
            {
                if (orderByLastNameAscending)
                {
                    orderedParticipants.ThenBy(p => p.lastName);
                }
                else
                {
                    orderedParticipants.ThenByDescending(p => p.lastName);
                }
            }
            if (orderByJobTitle)
            {
                if (orderByJobTitleAscending)
                {
                    orderedParticipants.ThenBy(p => p.jobTitle);
                }
                else
                {
                    orderedParticipants.ThenByDescending(p => p.jobTitle);
                }
            }
            if (orderByCompanyName)
            {
                if (orderByCompanyNameAscending)
                {
                    orderedParticipants.ThenBy(p => p.companyName);
                }
                else
                {
                    orderedParticipants.ThenByDescending(p => p.companyName);
                }
            }
            if (orderByCountry)
            {
                if (orderByCountryAscending)
                {
                    orderedParticipants.ThenBy(p => p.companyName);
                }
                else
                {
                    orderedParticipants.ThenByDescending(p => p.companyName);
                }
            }
            return orderedParticipants.ToList();
        }
    }
}
