using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

namespace TC37852369.Repository
{
    public class ParticipantRepository
    {
        public async Task<bool> addParticipant(Participant participant )
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Participant").Document(participant.participantId);
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id", long.Parse(participant.participantId) },
                { "EventId", participant.eventId },
                { "FirstName", participant.firstName },
                { "LastName", participant.lastName },
                { "JobTitle", participant.jobTitle },
                { "CompanyName", participant.companyName },
                { "CompanyType", participant.companyType },
                { "Email", participant.email },
                { "PhoneNumber", participant.phoneNumber },
                { "Country", participant.country },
                { "ParticipantFormat", participant.participationFormat },
                { "PaymentStatus", participant.paymentStatus },
                { "Materials", participant.materials },
                { "TicketBarcode", participant.ticketBarcode },
                { "TicketSent", participant.ticketSent },
                { "ParticipateEveningEvent", participant.participateEveningEvent },
                { "ParticipateInDay1", participant.participateInDay1 },
                { "ParticipateInDay2", participant.participateInDay2 },
                { "ParticipateInDay3", participant.participateInDay3 },
                { "ParticipateInDay4", participant.participateInDay4 },
                { "CheckedInDay1", participant.checkedInDay1 },
                { "CheckedInDay2", participant.checkedInDay2 },
                { "CheckedInDay3", participant.checkedInDay3 },
                { "CheckedInDay4", participant.checkedInDay4 }
            };
            WriteResult result = await docRef.SetAsync(user);
           
            return true;
        }
        public async Task<List<Participant>> getAllParticipants()
        {
            List<Participant> allPartcipants = new List<Participant>();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            Query allParticipantsQuery = db.Collection("Participant");
            QuerySnapshot allParticipantsQuerySnapshot = await allParticipantsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allParticipantsQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> ParticipantValue = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ParticipantValue)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }

                Participant ParticipantEntity = new Participant(
                        ParticipantValue["Id"].ToString(),
                        ParticipantValue["EventId"].ToString(),
                        ParticipantValue["FirstName"].ToString(),
                        ParticipantValue["LastName"].ToString(),
                        ParticipantValue["JobTitle"].ToString(),
                        ParticipantValue["CompanyName"].ToString(),
                        ParticipantValue["CompanyType"].ToString(),
                        ParticipantValue["Email"].ToString(),
                        ParticipantValue["PhoneNumber"].ToString(),
                        ParticipantValue["Country"].ToString(),
                        ParticipantValue["ParticipantFormat"].ToString(),
                        ParticipantValue["PaymentStatus"].ToString(),
                        Boolean.Parse(ParticipantValue["Materials"].ToString()),
                        ParticipantValue["TicketBarcode"].ToString(),
                        Boolean.Parse(ParticipantValue["TicketSent"].ToString()),
                        Boolean.Parse(ParticipantValue["ParticipateEveningEvent"].ToString()),
                        Boolean.Parse(ParticipantValue["ParticipateInDay1"].ToString()),
                        Boolean.Parse(ParticipantValue["ParticipateInDay2"].ToString()),
                        Boolean.Parse(ParticipantValue["ParticipateInDay3"].ToString()),
                        Boolean.Parse(ParticipantValue["ParticipateInDay4"].ToString()),
                        Boolean.Parse(ParticipantValue["CheckedInDay1"].ToString()),
                        Boolean.Parse(ParticipantValue["CheckedInDay2"].ToString()),
                        Boolean.Parse(ParticipantValue["CheckedInDay3"].ToString()),
                        Boolean.Parse(ParticipantValue["CheckedInDay4"].ToString())
                    );
                allPartcipants.Add(ParticipantEntity);
                Console.WriteLine("");
            }
            return allPartcipants;
        }
        public async Task<bool> deleteParticipant(string participantId)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            DocumentReference docRef = db.Collection("Participant").Document(participantId);
            await docRef.DeleteAsync();

            return true;
        }

    }
}
