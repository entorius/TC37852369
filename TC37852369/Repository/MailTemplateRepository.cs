using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

namespace TC37852369.Repository
{
    public class MailTemplateRepository
    {
        public async Task<bool> addMailTemplate(string id, string name, string subject, string body, bool is_Default)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            DocumentReference docRef = db.Collection("Mail_Template").Document("MailTemplate").Collection("MailTemplate").Document(id);

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "Name", name },
                { "Subject", subject },
                { "Body", body },
                { "Is_Default", is_Default }
            };
            await docRef.SetAsync(user);
            return true;
        }

        public async Task<List<EmailTemplate>> getAllMailTemplates()
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            List<EmailTemplate> emailTemplates = new List<EmailTemplate>();

            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);
            Query allEmailTemplateQuery = db.Collection("Mail_Template").Document("MailTemplate").Collection("MailTemplate");
            QuerySnapshot allEmailTemplateQuerySnapshot = await allEmailTemplateQuery.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in allEmailTemplateQuerySnapshot.Documents)
            {

                Dictionary<string, object> EmailTemplate = documentSnapshot.ToDictionary();

                EmailTemplate EmailTemplateEntity = new EmailTemplate(
                        EmailTemplate["Id"].ToString(),
                        EmailTemplate["Name"].ToString(),
                        EmailTemplate["Subject"].ToString(),
                        EmailTemplate["Body"].ToString(),
                        bool.Parse(EmailTemplate["Is_Default"].ToString())
                    );
                emailTemplates.Add(EmailTemplateEntity);
            }
            return emailTemplates;
        }

        public async Task<bool> deleteMailTemplate(string mail_Template_Id)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            DocumentReference docRef = db.Collection("Mail_Template").Document(mail_Template_Id);
            await docRef.DeleteAsync();

            return true;
        }

        public async Task<List<EmailTemplateString>> getEmailTemplateStrings()
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            List<EmailTemplateString> emailTemplateStrings = new List<EmailTemplateString>();

            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);
            Query allEmailTemplateStringsQuery = db.Collection("Mail_Template").Document("MailTemplateString").Collection("MailTemplateString");
            QuerySnapshot allEmailTemplateStringsQuerySnapshot = await allEmailTemplateStringsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allEmailTemplateStringsQuerySnapshot.Documents)
            {
               
                Dictionary<string, object> EmailTemplateString = documentSnapshot.ToDictionary();

                EmailTemplateString EmailTemplateStringEntity = new EmailTemplateString(
                        EmailTemplateString["title"].ToString(),
                        EmailTemplateString["dropString"].ToString()
                    );
                emailTemplateStrings.Add(EmailTemplateStringEntity);
            }

            return emailTemplateStrings;


        }
    }
}
