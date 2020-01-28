using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class EmailTemplate
    {
        public string id { set; get; }
        public string templateName { set; get; }
        public string subject { set; get; }
        public string body { set; get; }
        public bool isDefault { set; get; }

        public EmailTemplate(string id, string templateName, string subject, string body, bool isDefault)
        {
            this.id = id;
            this.templateName = templateName;
            this.subject = subject;
            this.body = body;
            this.isDefault = isDefault;
        }
    }
}
