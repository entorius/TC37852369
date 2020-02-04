using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Services.EmailSending
{
    public class SendEmail
    {
        private string fromEmail;
        private string fromPassword;
        EmailStringHelper emailStringHelper = new EmailStringHelper();
        
        public SendEmail(string fromEmail, string fromPassword)
        {
            this.fromEmail = fromEmail;
            this.fromPassword = fromPassword;
        }
        public static async Task<bool> TryToLogin(string email, string password)
        {
            ImapClient x = await Login(email, password);
            if(x == null)
            {
                return false;
            }
            return true;
        }
        public static async Task<ImapClient> Login(string username, string password)
        {
            var client = new ImapClient();
            try
            {
                client.Connect("imap.gmail.com", 993, true);
            }
            catch (Exception)
            {
                client = null;                   //blogas internetas arba atsijungęs serveris
            }

            if (client != null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        client.Authenticate(username, password);
                    }
                    catch (Exception)
                    {
                        client = null;

                    }
                });
            }
            return client;
        }
        public int SendEmails(List<string> toEmails, List<string> emailSubject, List<string> emailBody,
            List<string> attachmentsPaths, List<string> attachmentsNames)
        {
            int unsuccesses = 0;
            var fromAddress = new MailAddress(fromEmail, fromEmail);
            List<MailAddress> toAddresses = new List<MailAddress>();
            foreach (string email in toEmails)
            {
                if (!email.Contains(Environment.NewLine))
                {
                    toAddresses.Add(new MailAddress(email, email));
                }
            }
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            int lasti = 0;
            int totali = 0;
            for (int i = 0; i < toAddresses.Count; i++)
            {
                try
                {
                    var message = new MailMessage(fromAddress, toAddresses[i]);

                    message.Subject = emailSubject[i];
                    message.Body = emailBody[i];
                    Attachment attachment = new Attachment(attachmentsPaths[i]);
                    attachment.Name = attachmentsNames[i] + ".pdf";
                    message.Attachments.Add(attachment);
                    smtp.Send(message);
                    attachment.Dispose();
                        
                    totali = i;
                }
                catch (Exception)
                {
                    if (i - lasti >= 90)
                    {
                        //mainForm.Enabled = true;
                        //mainForm.Label_currentSatusSending.Text = "Išsiųsta laiškų: " + i + "Laukite 7 minutes kol atsinaujins siuntimas.";
                        //mainForm.Enabled = false;
                        System.Threading.Thread.Sleep(420000);
                        unsuccesses = unsuccesses + 1;
                        i = i - 1;
                        lasti = i;
                    }
                    else
                    {
                        List<string> succesfullEmails = new List<string>();
                        for (int j = 0; j <= i; j++)
                        {
                            succesfullEmails.Add(toEmails[j]);
                        }
                        i = toAddresses.Count;
                       // MailSended mailSendedWindow = new MailSended(succesfullEmails);
                        //mailSendedWindow.Show();
                        //mainForm.Label_currentSatusSending.Text = "Siuntimas baigtas!";
                        return succesfullEmails.Count;
                    }
                }

            }
            //mainForm.Label_currentSatusSending.Text = "Siuntimas baigtas!";
            return totali + 1;
        }
        private string MessageBodyFormat(string emailBody)
        {
            List<char> messageBodySplit = new List<char>();
            for (int i = 0; i < emailBody.Length; i++)
            {
                messageBodySplit.Add(emailBody[i]);
            }
            int messageBodyLength = messageBodySplit.Count;
            for (int i = 0; i < messageBodySplit.Count; i++)
            {
                if (messageBodySplit[i] == '\n')
                {
                    messageBodySplit.RemoveAt(i);
                    i = i - 1;
                }
                else
                {
                    i = messageBodyLength;
                }
            }
            string newBody = "";
            for (int i = 0; i < messageBodySplit.Count; i++)
            {
                newBody = newBody.Insert(newBody.Length, messageBodySplit[i].ToString());
            }
            return newBody;
        }
    }
}
