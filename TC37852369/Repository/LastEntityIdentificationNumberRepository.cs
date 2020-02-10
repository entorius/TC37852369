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
    public class LastEntityIdentificationNumberRepository
    {
        public async Task<LastIdentificationNumber> getLastIdetificationNumber(string domainEntityName)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            long databaseId;
            string databaseEntityName;
            LastIdentificationNumber lastIdetificationNumber = new LastIdentificationNumber();
            Query identificationNumberQuery = db.Collection("LastEntityIdentificationNumber").WhereEqualTo("EntityName", domainEntityName);
            QuerySnapshot identificationNumberQuerySnapshot = await identificationNumberQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in identificationNumberQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> identificationNumberDocument = documentSnapshot.ToDictionary();
                identificationNumberDocument.TryGetTypedValue("Id", out databaseId);
                identificationNumberDocument.TryGetTypedValue("EntityName", out databaseEntityName);

                lastIdetificationNumber.entityName = databaseEntityName;
                lastIdetificationNumber.id = databaseId;
            }
            return lastIdetificationNumber;
        }
        private async Task<LastIdentificationNumber> AddLastIdetificationNumber(LastIdentificationNumber lastIdentificationNumber)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            LastIdentificationNumber lastIdetificationNumber = new LastIdentificationNumber();
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id",                     lastIdentificationNumber.id                },
                { "EntityName",             lastIdentificationNumber.entityName              }           
            };
            DocumentReference docRef = db.Collection("LastEntityIdentificationNumber").Document(lastIdentificationNumber.entityName + "Identification");
            await docRef.SetAsync(user);
            return lastIdetificationNumber;
        }
        public async Task<LastIdentificationNumber> IncreaseLastIdetificationNumber(string domainEntityName)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            LastIdentificationNumber lastIdentificationNumber = await getLastIdetificationNumber(domainEntityName);
            lastIdentificationNumber.id += 1;
            await AddLastIdetificationNumber(lastIdentificationNumber);
            return lastIdentificationNumber;
        }
        
    }
}
