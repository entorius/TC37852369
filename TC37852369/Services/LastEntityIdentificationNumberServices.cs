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
        public async Task<LastIdentificationNumber> getLastIdetificationNumber(string domainEntityName)
        {
            return await lastEntityIdentificationNumberRepository.getLastIdetificationNumber(domainEntityName);
        }
        public async Task<LastIdentificationNumber> IncreaseLastIdetificationNumber(string domainEntityName)
        {
            return await lastEntityIdentificationNumberRepository.IncreaseLastIdetificationNumber(domainEntityName);
        }
    }
}
