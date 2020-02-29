using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Repository;

public enum EventStatus
{
    Upcoming,
    Ongoing,
    Past

}

namespace TC37852369.Services
{
    public class EventServices
    {
        EventRepository eventRepository = new EventRepository();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        public async Task<Event> addEvent(string eventName, DateTime date_From, 
            int eventLengthDays, DateTime day1Date, DateTime day2Date, DateTime day3Date,
            DateTime day4Date, string day1TimeFrom,string day1TimeTo,
                   string day2TimeFrom, string day2TimeTo, string day3TimeFrom, string day3TimeTo,
                   string day4TimeFrom, string day4TimeTo, string webPage, string venueName,
                   string venueAdress, string eventStatus, string comment, bool useTemplate, 
                   string current_Mail_Template, string body, string subject)
        {

            LastIdentificationNumber lastIdentificationNumber = 
                await lastEntityIdentificationNumberServices.getEventLastIdentificationNumber();
            Event eventEntity = new Event(lastIdentificationNumber.id.ToString(), eventName,
                date_From, eventLengthDays, day1Date, day2Date, day3Date, day4Date,
                day1TimeFrom, day1TimeTo,
                   day2TimeFrom, day2TimeTo, day3TimeFrom, day3TimeTo, day4TimeFrom,
                   day4TimeTo,webPage, venueName, venueAdress, eventStatus, comment, useTemplate,
                current_Mail_Template, body, subject
                );
            bool isRequestSucessful = await eventRepository.addEvent(lastIdentificationNumber.id, 
                eventName,date_From, eventLengthDays, day1Date, day2Date, day3Date, day4Date,
                 day1TimeFrom, day1TimeTo,
                   day2TimeFrom, day2TimeTo, day3TimeFrom, day3TimeTo, day4TimeFrom,
                   day4TimeTo, webPage, venueName,venueAdress, eventStatus,comment, useTemplate,
                current_Mail_Template,body,subject);
            if (isRequestSucessful)
            {
                return eventEntity;
            }
            else
            {
                return null;
            }
        }
        public async Task<Event> editEvent(Event eventEntity)
        {
           
            bool isRequestSucessful = await eventRepository.addEvent(eventEntity.id,
                eventEntity.eventName, eventEntity.date_From, eventEntity.eventLengthDays, eventEntity.day1Date, eventEntity.day2Date, eventEntity.day3Date, eventEntity.day4Date,
                 eventEntity.day1TimeFrom, eventEntity.day1TimeTo,
                   eventEntity.day2TimeFrom, eventEntity.day2TimeTo, eventEntity.day3TimeFrom, eventEntity.day3TimeTo, eventEntity.day4TimeFrom,
                   eventEntity.day4TimeTo, eventEntity.webPage, eventEntity.venueName, eventEntity.venueAdress, eventEntity.eventStatus,
                   eventEntity.comment, eventEntity.useTemplate,
                eventEntity.current_Mail_Template, eventEntity.emailBody, eventEntity.emailSubject);
            if (isRequestSucessful)
            {
                return eventEntity;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> deleteEvent(long event_Id)
        {
            return await eventRepository.deleteEvent(event_Id);
        }
        public async Task<List<Event>> getAllEvents()
        {
            return await eventRepository.getAllEvents();
        }


        /*public async Task<bool> addEventDay(string event_Day_Id, string id, string event_Id, int dayNum, double cost, DateTime event_Day_Date)
        {
            return await eventRepository.addEventDay(event_Day_Id, id, event_Id,dayNum,cost,event_Day_Date);
        }
        public async Task<bool> deleteEventDay(string event_Id, string Event_Day_Id)
        {
            return await eventRepository.deleteEventDay(event_Id,Event_Day_Id);
        }*/

        public string getEventStatus(DateTime eventTime, int eventDuration)
        {
            if(eventTime.AddDays(eventDuration - 1) < DateHelper.setDateToMidnight(DateTime.Now))
            {
                return nameof(EventStatus.Past);
            }
            else if (eventTime <= DateHelper.setDateToMidnight(DateTime.Now) && 
                eventTime.AddDays(eventDuration - 1) >= DateHelper.setDateToMidnight(DateTime.Now))
            {
                return nameof(EventStatus.Ongoing);
            }
            else
            {
                return nameof(EventStatus.Upcoming);
            }
        }




        public bool isStringCorrect(string value)
        {
            value = value.Replace(" ", string.Empty);
            if(value.Length == 0)
            {
                return true;
            }
            if(value.Length < 3)
            {
                return false;
            }
            return true;
        }

        public int isEventDatesCorrect(DateTime eventDate,int eventDuration,
            DateTime day1Date, DateTime day2Date, DateTime day3Date, DateTime day4Date)
        {
            if(eventDuration == 0)
            {
                return 5;
            }
            if (day4Date <= day3Date && eventDuration > 3)
            {
                return 4;
            }
            if (day3Date <= day2Date && eventDuration > 2)
            {
                return 3;
            }
            if (day2Date <= day1Date && eventDuration > 1)
            {
                return 2;
            }
            if (day1Date != eventDate && eventDuration > 0)
            {
                return 1;
            }
            return 0;
        }

        public bool isTimeFromToCorrect(int fromHour,int fromMinute,int toHour,int toMinute)
        {
            if(fromHour*60+fromMinute < toHour * 60 + toMinute)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int isSomeTimeFromToNotCorrect(int fromDay1Hour, int fromDay1Minute, 
            int toDay1Hour, int toDay1Minute, int fromDay2Hour, int fromDay2Minute,
            int toDay2Hour, int toDay2Minute, int fromDay3Hour, int fromDay3Minute,
            int toDay3Hour, int toDay3Minute, int fromDay4Hour, int fromDay4Minute,
            int toDay4Hour, int toDay4Minute, int eventDuration)
        {
            if(!isTimeFromToCorrect(fromDay1Hour, fromDay1Minute, toDay1Hour, toDay1Minute) && eventDuration >0)
            {
                return 1;
            }
            else if (!isTimeFromToCorrect(fromDay2Hour, fromDay2Minute, toDay2Hour, toDay2Minute) && eventDuration > 1)
            {
                return 2;
            }
            else if (!isTimeFromToCorrect(fromDay3Hour, fromDay3Minute, toDay3Hour, toDay3Minute) && eventDuration > 2)
            {
                return 3;
            }
            else if (!isTimeFromToCorrect(fromDay4Hour, fromDay4Minute, toDay4Hour, toDay4Minute) && eventDuration > 3)
            {
                return 4;
            }
            else return 0;

        }

        public int isEmailTemplateCorrect(bool useDefaultEmail, string emailSubject, string emailBody, string emailTemplate)
        {
            if (useDefaultEmail)
            {
               if(emailTemplate.Length < 1)
                {
                    return 1;
                }
            }
            else
            {
                if (!isStringCorrect(emailSubject))
                {
                    return 2;
                }
                if (!isStringCorrect(emailBody))
                {
                    return 3;
                }
            }
            return 0;
        }
        public string formHoursAndMinutesString(string hours,string minutes)
        {
            return hours + ":" + minutes;
        }
        public int checkIfPaymentAmountCorrect(string paymentAmount)
        {
            double paymentAmountConverted = 0;
            try
            {
                paymentAmountConverted = Double.Parse(paymentAmount);
            }
            catch (ArgumentNullException)
            {
                return 1;
            }
            catch (FormatException)
            {
                return 2;
            }
            catch (OverflowException)
            {
                return 3;
            }
            return 0;
        }
        public List<Event> filterEvents(List<Event> allEvents,int year,int month,int day, string eventName)
        {
            List<Event> filteredEvents = allEvents;
            if (year != 0)
            {
                filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Year == year || ev.date_From.AddDays(ev.eventLengthDays).Year == year);

            }
            if (month != 0)
            {
                filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Month == month || ev.date_From.AddDays(ev.eventLengthDays).Month == month);

            }
            if (day != 0)
            {
                filteredEvents = filteredEvents.FindAll(ev => ev.date_From.Day <= day || ev.date_From.AddDays(ev.eventLengthDays).Day >= day);
            }
            if(eventName.Replace(" ", "").Length > 0)
            {
                filteredEvents = filteredEvents.FindAll(ev => ev.eventName.Replace(" ","").Equals(eventName.Replace(" ","")));
            }
            return filteredEvents;
        }
        public bool eventDayCorrect(string day)
        {
            int dayNumber = 0;
            bool dayParsed = Int32.TryParse(day, out dayNumber);
            if (dayParsed)
            {
                if (dayNumber <= 31 && dayNumber >= 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool eventYearCorrect(string year)
        {
            int yearNumber = 0;
            bool yearParsed = Int32.TryParse(year, out yearNumber);
            if (yearParsed)
            {
                if (yearNumber >= 1800)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool eventMonthCorrect(string month)
        {
            int monthNumber = 0;
            bool dayParsed = Int32.TryParse(month, out monthNumber);
            if (dayParsed)
            {
                if (monthNumber <= 12 && monthNumber >= 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public int getEventDayNumber(Event eventEntity)
        {
            for (int i = 0; i < eventEntity.eventLengthDays; i++)
            {
                if (DateHelper.setDateToMidnight(DateTime.Now).Equals(eventEntity.date_From.AddDays(i)))
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
