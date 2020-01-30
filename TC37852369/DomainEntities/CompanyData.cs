using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class CompanyData
    {
        public string address { set; get; }
        public string companyName { set; get; }
        public string email { set; get; }
        public string phoneNumber { set; get; }
        public string webPageAddress { set; get; }
        public string companyLogo { set; get; }
        public string emailUsername { get; set; }
        public string emailPassword { get; set; }

        public CompanyData(string address, string companyName, string email, 
            string phoneNumber, string webPageAddress, string comapnyLogo, string emailUsername,
            string emailPassword)
        {
            this.address = address;
            this.companyName = companyName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.webPageAddress = webPageAddress;
            this.companyLogo = comapnyLogo;
            this.emailUsername = emailUsername;
            this.emailPassword = emailPassword;

        }
    }
}
