using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Repository
{
    public class MailTemplateRepository
    {
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
