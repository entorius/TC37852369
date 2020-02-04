using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Helpers;

namespace TC37852369.Repository
{
    public class UserRepository
    {
        SetEnvironmentVariable SetEnvironmentVariable = new SetEnvironmentVariable();
        public async Task<bool> addUser(string username, string password, string mail,string phoneNumber, string name, string surename, long user_Id)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("System_User").Document(user_Id.ToString());
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", user_Id },
                { "Mail", mail },
                { "PhoneNumber", phoneNumber },
                { "Name", name },
                { "Surename", surename },
                { "Password", password },
                { "Username", username }
            };
            await docRef.SetAsync(user);

            return true;
        }
        public async Task<User> GetUser(string username, string password)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            User user = new User();
            string databaseId;
            string databaseMail;
            string databasePhoneNumber;
            string databaseSurename;
            string databaseName;
            string databasePassword;
            string databaseUsername;
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            Query userQuery = db.Collection("System_User").WhereEqualTo("Username", username).WhereEqualTo("Password", password);
            QuerySnapshot userQuerySnapshot = await userQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in userQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> userDocument = documentSnapshot.ToDictionary();
                userDocument.TryGetTypedValue("Id", out databaseId);
                userDocument.TryGetTypedValue("Mail", out databaseMail);
                userDocument.TryGetTypedValue("PhoneNumber", out databasePhoneNumber);
                userDocument.TryGetTypedValue("Name", out databaseName);
                userDocument.TryGetTypedValue("Surename", out databaseSurename);
                userDocument.TryGetTypedValue("Password", out databasePassword);
                userDocument.TryGetTypedValue("Username", out databaseUsername);

                user.setUserData(databaseId, databaseMail, databasePhoneNumber, databaseName, databaseSurename, databasePassword, databaseUsername);
            }
            return user;
        }

        public async Task<bool> deleteUser(string user_Id)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("System_User").Document(user_Id);
            await docRef.DeleteAsync();

            return true;
        }
    }
}
