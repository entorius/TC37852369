using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    class LastEntityIdentificationNumberServices
    {
        LastEntityIdentificationNumberRepository lastEntityIdentificationNumberRepository =
            new LastEntityIdentificationNumberRepository();
        private async Task<LastIdentificationNumber> getLastIdetificationNumber(string domainEntityName)
        {
            return await lastEntityIdentificationNumberRepository.getLastIdetificationNumber(domainEntityName);
        }
        public async Task<LastIdentificationNumber> IncreaseLastIdetificationNumber(string domainEntityName)
        {
            return await lastEntityIdentificationNumberRepository.IncreaseLastIdetificationNumber(domainEntityName);
        }
        public async Task<LastIdentificationNumber> getEventLastIdentificationNumber()
        {
            return await this.getLastIdetificationNumber("Event");
        }
        public async Task<LastIdentificationNumber> getMailTemplateLastIdetificationNumber()
        {
            return await this.getLastIdetificationNumber("MailTemplate");
        }
        public async Task<LastIdentificationNumber> getUserLastIdentificationNumber()
        {
            return await this.getLastIdetificationNumber("User");
        }
        public async Task<LastIdentificationNumber> getParticipantLastIdentificationNumber()
        {
            return await this.getLastIdetificationNumber("Participant");
        }
        public async Task<LastIdentificationNumber> getBarcodeLastIdentificationNumber()
        {
            return await this.getLastIdetificationNumber("Barcode");
        }
        public async Task<LastIdentificationNumber> getImageEntityLastIdentificationNumber()
        {
            return await this.getLastIdetificationNumber("ImageEntity");
        }
    }
}
