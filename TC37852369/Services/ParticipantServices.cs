using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;
using TC37852369.Services.Ticket_generation;

namespace TC37852369.Services
{
    public class ParticipantServices
    {
        LastEntityIdentificationNumberServices lastIdentificationNumber = new LastEntityIdentificationNumberServices();
        ParticipantRepository participantRepository = new ParticipantRepository();
        BarcodeGenerator barcodeGenerator = new BarcodeGenerator();
        public async Task<Participant> addParticipant(string participantId, string event_Id,
            string firstName, string lastName,string jobTitle, string company_Name, string companyType,
            string email, string phone_Number, string country, string participation_Format,
            string payment_Status, bool materials, string ticket_Barcode, bool ticket_Sent,
            bool participate_Evening_Event, bool participate_In_Day1, bool participate_In_Day2,
            bool participate_In_Day3, bool participate_In_Day4, bool checked_In_Day1,
            bool checked_In_Day2, bool checked_In_Day3, bool checked_In_Day4)
        {
           
            Participant participant = new Participant(participantId, event_Id,
             firstName, lastName,jobTitle, company_Name, companyType,
             email, phone_Number, country, participation_Format,
             payment_Status, materials, ticket_Barcode, ticket_Sent,
             participate_Evening_Event, participate_In_Day1, participate_In_Day2,
             participate_In_Day3, participate_In_Day4, checked_In_Day1,
             checked_In_Day2, checked_In_Day3, checked_In_Day4);


            bool participantAdded = await participantRepository.addParticipant(participant);
            if (participantAdded)
            {
                return participant;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Participant>> getAllParticipants()
        {
            return await participantRepository.getAllParticipants();
        }
        public async Task<Participant> getParticipant(string Id)
        {
            return await participantRepository.getParticipant(Id);
        }
        public async Task<Participant> createParticipant( string event_Id,
        string firstName, string lastName, string jobTitle, string company_Name, string companyType,
        string email, string phone_Number, string country, string participation_Format,
        string payment_Status, bool materials, 
        bool participate_Evening_Event, bool participate_In_Day1, bool participate_In_Day2,
        bool participate_In_Day3, bool participate_In_Day4)
        {
            LastIdentificationNumber participant_Id = await lastIdentificationNumber.getParticipantLastIdentificationNumber();

            string barcode = await barcodeGenerator.generateBarcodeNumber(participant_Id.id.ToString());

            await lastIdentificationNumber.IncreaseLastIdetificationNumber("Participant");

            return await this.addParticipant(participant_Id.id.ToString(), event_Id,
             firstName,  lastName, jobTitle,  company_Name,  companyType,
             email,  phone_Number,  country,  participation_Format,
             payment_Status,  materials, barcode,  false,
             participate_Evening_Event,  participate_In_Day1,  participate_In_Day2,
             participate_In_Day3,  participate_In_Day4,  false,
             false,  false,  false);
        }

        public async Task<Participant> editParticipant(Participant participant)
        {
            
            return await this.addParticipant(participant.participantId, participant.eventId,
             participant.firstName, participant.lastName, participant.jobTitle, 
             participant.companyName, participant.companyType,
             participant.email, participant.phoneNumber, participant.country, 
             participant.participationFormat, participant.paymentStatus, participant.materials, 
             participant.ticketBarcode, participant.ticketSent,
             participant.participateEveningEvent, participant.participateInDay1, participant.participateInDay2,
             participant.participateInDay3, participant.participateInDay4, participant.checkedInDay1,
             participant.checkedInDay2, participant.checkedInDay3, participant.checkedInDay4);
        }

        public async Task<bool> deleteParticipant(string participantId)
        {
            return await participantRepository.deleteParticipant(participantId);
        }

        public double countPaymentAmount(double paymentAmount, int daysParticipating)
        {
            return paymentAmount * daysParticipating;
        }

        //Counts in how many days of the event participant going
        public int countInHowManyDaysParticipating(Participant participant)
        {
            return new[] { participant.participateInDay1,
                participant.participateInDay2, participant.participateInDay3,
                participant.participateInDay4 }.Count(x => x);
        }
        public int isPartcipantInformationManditoryFieldsCorrect(string name, string surename, string email)
        {

            if (name.Length < 3)
            {
                return 1;
            }
            else if (surename.Length < 3)
            {
                return 2;
            }
            try
            {
                MailAddress emailCatch = new MailAddress(email);
            }
            catch (FormatException)
            {
                return 3;
            }
            catch (ArgumentNullException)
            {
                return 3;
            }
            catch (ArgumentException)
            {
                return 3;
            }
            return 0;
        }
    }
}
