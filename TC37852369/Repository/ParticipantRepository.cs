using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Repository
{
    public class ParticipantRepository
    {
        public async Task<bool> addParticipant(string participant_Id, string name, string surename,
    string email_Address, string event_Id, string industry_Service, string company_Name, string participation_Format,
    bool paid, bool ticket_Sent, string ticket_ID, bool participation_Day_One, bool participation_Day_Two)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Participant").Document(participant_Id);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", participant_Id },
                { "Name", name },
                { "Surename", surename },
                { "Email_Address", email_Address },
                { "Event_ID", event_Id },
                { "Industry_Service", industry_Service },
                { "Company_Name", company_Name },
                { "Participant_Format", participation_Format },
                { "Paid", paid },
                { "Ticket_Sent", ticket_Sent },
                { "Ticket_ID", ticket_ID },
                { "Participant_Day_One", participation_Day_One },
                { "Participant_Day_Two", participation_Day_Two }
            };
            await docRef.SetAsync(user);
            return true;
        }
        public async Task<bool> deleteParticipant(string event_Id, string participantId)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Participant").Document(participantId);
            await docRef.DeleteAsync();

            return true;
        }
    }
}
