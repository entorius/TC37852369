using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Repository
{
    public class EventRepository
    {
        public async Task<bool> addEvent(long event_Id, DateTime date_From, DateTime date_To, string Address, string Name, string isEnded, string current_Mail_Template)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            Timestamp dateFromStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(date_From);
            Timestamp dateToStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(date_To);

            DocumentReference docRef = db.Collection("Event").Document(event_Id.ToString());
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", event_Id },
                { "DateFrom", dateFromStamp },
                { "DateTo", dateToStamp },
                { "Ended", isEnded },
                { "Name", Name },
                { "current_Mail_Template", current_Mail_Template }
            };
            await docRef.SetAsync(user);
            return true;
        }

        public async Task<bool> deleteEvent(long event_Id)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Event").Document(event_Id.ToString());
            await docRef.DeleteAsync();

            return true;
        }
        public async Task<bool> addEventDay(string event_Day_Id, string id, string event_Id, int dayNum, double cost, DateTime event_Day_Date)
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
    }
}
