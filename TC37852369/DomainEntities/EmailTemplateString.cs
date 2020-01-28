using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class EmailTemplateString
    {
        public string name { set; get; }
        public string value { set; get; }

        public EmailTemplateString(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

    }
}
