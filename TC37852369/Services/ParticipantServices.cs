using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class ParticipantServices
    {
        ParticipantRepository participantRepository = new ParticipantRepository();
        public async Task<bool> addParticipant(string id, string name, string surename,
   string email_Address, string event_Id, string industry_Service, string company_Name, string participation_Format,
   bool paid, bool ticket_Sent, string ticket_ID, bool participation_Day_One, bool participation_Day_Two)
        {
            return await participantRepository.addParticipant(id, name, surename,
            email_Address, event_Id, industry_Service, company_Name, participation_Format,
            paid, ticket_Sent, ticket_ID, participation_Day_One, participation_Day_Two);
        }
        public async Task<bool> deleteParticipant(string event_Id, string participantId)
        {
            return await participantRepository.deleteParticipant(event_Id,participantId);
        }
    }
}
