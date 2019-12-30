using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class EventServices
    {
        EventRepository eventRepository = new EventRepository();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        public async Task<bool> addEvent(string event_Id, string id, DateTime date_From, DateTime date_To, string Address, string Name, string isEnded, string current_Mail_Template)
        {

            LastIdentificationNumber lastIdentificationNumber = await lastEntityIdentificationNumberServices.getLastIdetificationNumber("Event");
            return await eventRepository.addEvent(lastIdentificationNumber.id,date_From,date_To,Address,Name,isEnded,current_Mail_Template);
        }

        public async Task<bool> deleteEvent(long event_Id)
        {
            return await eventRepository.deleteEvent(event_Id);
        }

        public async Task<bool> addEventDay(string event_Day_Id, string id, string event_Id, int dayNum, double cost, DateTime event_Day_Date)
        {
            return await eventRepository.addEventDay(event_Day_Id, id, event_Id,dayNum,cost,event_Day_Date);
        }
        public async Task<bool> deleteEventDay(string event_Id, string Event_Day_Id)
        {
            return await eventRepository.deleteEventDay(event_Id,Event_Day_Id);
        }
    }
}
