using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

namespace TC37852369.Repository
{
    public class EventRepository
    {
        public async Task<bool> addEvent(long event_Id, string eventName, DateTime date_From,
            int eventLengthDays, DateTime day1Date, DateTime day2Date, DateTime day3Date,
            DateTime day4Date, string day1TimeFrom, string day1TimeTo,
            string day2TimeFrom, string day2TimeTo, string day3TimeFrom, string day3TimeTo,
            string day4TimeFrom, string day4TimeTo, 
            string venueName, string venueAddress,  string eventStatus,string comment,
            bool useTemplate, string current_Mail_Template, string body, string subject)
        {
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");

            string dateFromString = date_From.ToString();

            string day1DateString = day1Date.ToString();

            string day2DateString = day2Date.ToString();

            string day3DateString = day3Date.ToString();

            string day4DateString = day4Date.ToString();


            DocumentReference docRef = db.Collection("Event").Document(event_Id.ToString());
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "Id",                     event_Id                },
                { "EventName",              eventName               },
                { "DateFrom",               dateFromString           },
                { "EventLengthDays",        eventLengthDays         },
                { "DateDay1",               day1DateString           },
                { "DateDay2",               day2DateString           },
                { "DateDay3",               day3DateString           },
                { "DateDay4",               day4DateString           },
                { "Day1TimeFrom",           day1TimeFrom            },
                { "Day1TimeTo",             day1TimeTo              },
                { "Day2TimeFrom",           day2TimeFrom            },
                { "Day2TimeTo",             day2TimeTo              },
                { "Day3TimeFrom",           day3TimeFrom            },
                { "Day3TimeTo",             day3TimeTo              },
                { "Day4TimeFrom",           day4TimeFrom            },
                { "Day4TimeTo",             day4TimeTo              },
                { "VenueName",              venueName               },
                { "VenueAdress",            venueAddress            },
                { "EventStatus",            eventStatus             },
                { "Comment",                comment                 }, 
                { "UseTemplate",            useTemplate             },  
                { "Current_Mail_Template",  current_Mail_Template   },
                { "EmailBody",              body                    },
                { "EmailSubject",           subject                 }
            };
            await docRef.SetAsync(user);
            return true;
        }

        public async Task<List<Event>> getAllEvents()
        {
            List<Event> allEvents = new List<Event>();
            FirestoreDb db = FirestoreDb.Create("ticketbase-36d66");
            Query allEventsQuery = db.Collection("Event");
            QuerySnapshot allEventsQuerySnapshot = await allEventsQuery.GetSnapshotAsync();
            foreach(DocumentSnapshot documentSnapshot in allEventsQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> eventValue = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in eventValue)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }

                Event eventEntity = new Event(
                        eventValue["Id"].ToString(),
                        eventValue["EventName"].ToString(),
                        DateTime.Parse(eventValue["DateFrom"].ToString()),
                        Int32.Parse(eventValue["EventLengthDays"].ToString()),
                        DateTime.Parse(eventValue["DateDay1"].ToString()),
                        DateTime.Parse(eventValue["DateDay2"].ToString()),
                        DateTime.Parse(eventValue["DateDay3"].ToString()),
                        DateTime.Parse(eventValue["DateDay4"].ToString()),
                        eventValue["Day1TimeFrom"].ToString(),
                        eventValue["Day1TimeTo"].ToString(),
                        eventValue["Day2TimeFrom"].ToString(),
                        eventValue["Day2TimeTo"].ToString(),
                        eventValue["Day3TimeFrom"].ToString(),
                        eventValue["Day3TimeTo"].ToString(),
                        eventValue["Day4TimeFrom"].ToString(),
                        eventValue["Day4TimeTo"].ToString(),
                        eventValue["VenueName"].ToString(),
                        eventValue["VenueAdress"].ToString(),
                        eventValue["EventStatus"].ToString(),
                        eventValue["Comment"].ToString(),
                        Boolean.Parse(eventValue["UseTemplate"].ToString()),
                        eventValue["Current_Mail_Template"].ToString(),
                        eventValue["EmailBody"].ToString(),
                        eventValue["EmailSubject"].ToString()
                    );
                allEvents.Add(eventEntity);
                Console.WriteLine("");
            }

                    return allEvents;
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
