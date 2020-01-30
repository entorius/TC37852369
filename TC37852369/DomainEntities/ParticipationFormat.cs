using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class ParticipationFormat
    {
        public string Id { set; get; }
        public string Value { set; get; }

        public ParticipationFormat(string id, string value)
        {
            this.Id = id;
            this.Value = value;
        }
    }
}
