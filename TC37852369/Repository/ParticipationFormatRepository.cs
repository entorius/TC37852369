using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

namespace TC37852369.Repository
{
    public class ParticipationFormatRepository
    {
        public async Task<bool> addParticipationFormat(string id, string value)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            DocumentReference docRef = db.Collection("ParticipationFormat").Document(id);

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", id },
                { "Value", value }
            };
            await docRef.SetAsync(user);
            return true;
        }

        public async Task<List<ParticipationFormat>> getAllParticipationFormats()
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            List<ParticipationFormat> participationFormats = new List<ParticipationFormat>();

            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);
            Query allParticipationFormatQuery = db.Collection("ParticipationFormat");
            QuerySnapshot allParticipationFormatQuerySnapshot = await allParticipationFormatQuery.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in allParticipationFormatQuerySnapshot.Documents)
            {

                Dictionary<string, object> participationFormat = documentSnapshot.ToDictionary();

                ParticipationFormat ParticipationFormatEntity = new ParticipationFormat(
                        participationFormat["Id"].ToString(),
                        participationFormat["Value"].ToString()
                    );
                participationFormats.Add(ParticipationFormatEntity);
            }
            return participationFormats;
        }

        public async Task<bool> deleteParticipationFormat(string id)
        {
            SetEnvironmentVariable.setFirestoreEnvironmentVariable();
            FirestoreDb db = FirestoreDb.Create(GetConstant.FIRESTORE_ID);

            DocumentReference docRef = db.Collection("ParticipationFormat").Document(id);
            await docRef.DeleteAsync();

            return true;
        }

    }
}
