using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class MailTemplateServices
    {
        MailTemplateRepository mailTemplateRepository = new MailTemplateRepository();

        public async Task<bool> addMailTemplate(string documentId, string id, string name, string subject, string body, string event_ID, bool is_Default)
        {
            return await mailTemplateRepository.addMailTemplate(documentId,id,name,subject,body,event_ID,is_Default);
        }
        public async Task<bool> deleteMailTemplate(string mail_Template_Id)
        {
            return await mailTemplateRepository.deleteMailTemplate(mail_Template_Id);
        }
    }
}
