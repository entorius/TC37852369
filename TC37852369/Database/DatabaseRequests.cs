using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Google.Cloud.Firestore;




namespace TC37852369.Database
{
    public class DatabaseRequests
    {
        public async Task<bool> addUser(string id, string username, string password, string mail, string name, string user_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("System_User").Document(user_Id);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "Mail", mail },
                { "Name", name },
                { "Password", password },
                { "Username", username }
            };
            await docRef.SetAsync(user);

            return true;
        }

        public async Task<bool> deleteUser(string user_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("System_User").Document(user_Id);
            await docRef.DeleteAsync();

            return true;
        }

        public async Task<bool> addEvent(string event_Id,string id, DateTime date_From, DateTime date_To, string Address, string Name, string isEnded, string current_Mail_Template)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            Timestamp dateFromStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(date_From);
            Timestamp dateToStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(date_To);

            DocumentReference docRef = db.Collection("Event").Document(event_Id);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "DateFrom", dateFromStamp },
                { "DateTo", dateToStamp },
                { "Ended", isEnded },
                { "Id", id },
                { "Name", Name },
                { "current_Mail_Template", current_Mail_Template }
            };
            await docRef.SetAsync(user);
            return true;
        }

        public async Task<bool> deleteEvent(string event_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id);
            await docRef.DeleteAsync();

            return true;
        }

        public async Task<bool> addParticipant(string participant_Id, string id, string name, string surename, 
            string email_Address, string event_Id, string industry_Service, string company_Name, string participation_Format,
            bool paid, bool ticket_Sent, string ticket_ID,bool participation_Day_One, bool participation_Day_Two)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Participant").Document(participant_Id);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
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
        public async Task<bool> deleteParticipantt(string event_Id, string participantId)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Participant").Document(participantId);
            await docRef.DeleteAsync();

            return true;
        }
        public async Task<bool> addEventDay(string event_Day_Id, string id, string event_Id, int dayNum, double cost,DateTime event_Day_Date)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Event_Day").Document(event_Day_Id);
            Timestamp eventDateStamp = new Timestamp();
            eventDateStamp = Timestamp.FromDateTime(event_Day_Date);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "Event_Id", event_Id },
                { "Day_Number", dayNum },
                { "Cost", cost },
                { "Event_Day_Date", eventDateStamp }
            };
            await docRef.SetAsync(user);
            return true;
        }
        public async Task<bool> deleteEventDay(string event_Id, string Event_Day_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id).Collection("Event_Day").Document(Event_Day_Id);
            await docRef.DeleteAsync();

            return true;
        }
        public async Task<bool> addMailTemplate(string documentId, string id, string name, string subject, string body, string event_ID, bool is_Default)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Mail_Template").Document(documentId);

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "Name", name },
                { "Subject", subject },
                { "Body", body },
                { "Event_ID", event_ID },
                { "Is_Default", is_Default }
            };
            await docRef.SetAsync(user);
            return true;
        }
        public async Task<bool> deleteMailTemplate(string mail_Template_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Mail_Template").Document(mail_Template_Id);
            await docRef.DeleteAsync();

            return true;
        }
    }
}
