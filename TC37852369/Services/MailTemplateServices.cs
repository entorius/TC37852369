using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class MailTemplateServices
    {
        MailTemplateRepository mailTemplateRepository = new MailTemplateRepository();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumber = new LastEntityIdentificationNumberServices();

        private async Task<EmailTemplate> addMailTemplate( string id, string name, string subject, string body, bool is_Default)
        {
            bool created = await mailTemplateRepository.addMailTemplate(id, name, subject, body, is_Default);
            if (created) {
                return new EmailTemplate(id, name, subject, body, is_Default);
            }
            else
            {
                return null;
            }
        }

        public async Task<EmailTemplate> createMailTemplate(string name, string subject, string body, bool is_Default)
        {
            LastIdentificationNumber number = await lastEntityIdentificationNumber.getMailTemplateLastIdetificationNumber();
            await lastEntityIdentificationNumber.IncreaseLastIdetificationNumber("MailTemplate");
            return await addMailTemplate(number.id.ToString(), name, subject, body, is_Default);
        }
        public async Task<EmailTemplate> editMailTemplate(string id, string name, string subject, string body, bool is_Default)
        {

            return await addMailTemplate(id, name, subject, body, is_Default);
        }
        public async Task<bool> deleteMailTemplate(string mail_Template_Id)
        {
            return await mailTemplateRepository.deleteMailTemplate(mail_Template_Id);
        }
        public async Task<List<EmailTemplateString>> getEmailTemplateStrings()
        {
            return await mailTemplateRepository.getEmailTemplateStrings();
        }
        public async Task<List<EmailTemplate>> getAllMailTemplates()
        {
            return await mailTemplateRepository.getAllMailTemplates();
        }
    }
}
