using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Cloud.Firestore;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;



namespace TC37852369.Database
{
    public class DatabaseRequests
    {
        
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "mtmfz13yNJJLuio1yYMKH1jbSQRIV5anaTXv5C4O",
            BasePath = "https://ticketbase-36d66.firebaseio.com/"
        };
        IFirebaseClient client;

        public DatabaseRequests()
        {
            client = new FireSharp.FirebaseClient(config);
        } 
        public async Task<bool> addUser(string id, string username, string password, string mail, string name, string userInfo)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("System_User").Document(userInfo);
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

        public async Task<bool> addEvent(string documentId,string id, DateTime dateFrom, DateTime dateTo, string Address, string Name, string isEnded, string current_Mail_Template)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            Timestamp dateFromStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(dateFrom);
            Timestamp dateToStamp = new Timestamp();
            dateFromStamp = Timestamp.FromDateTime(dateTo);

            DocumentReference docRef = db.Collection("Event").Document(documentId);
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
    }
}
