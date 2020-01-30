using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class ParticipationFormatServices
    {
        ParticipationFormatRepository participationFormatRepository = new ParticipationFormatRepository();
        public async Task<ParticipationFormat> addParticipationFormat(string value)
        {
            string id = value.Replace(".", "");
            id = id.Replace("$", "");
            id = id.Replace("[", "");
            id = id.Replace("]", "");
            id = id.Replace("#", "");
            id = id.Replace("/", "");
            bool success = await participationFormatRepository.addParticipationFormat(id,value);

            if (success)
            {
                return new ParticipationFormat(id, value);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ParticipationFormat>> getAllParticipationFormats()
        {

            return await participationFormatRepository.getAllParticipationFormats();
        }

        public async Task<bool> deleteParticipationFormat(string id)
        {
            return await participationFormatRepository.deleteParticipationFormat(id);
        }
    }
}
