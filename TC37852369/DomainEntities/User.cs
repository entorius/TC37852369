using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DomainEntities
{
    public class User
    {
        public string id { get; set; }
        public string mail { get; set; }
        public string phoneNumber { get; set; }
        public string name { get; set; }
        public string surename { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public void setUserData(string id,string mail, string phoneNumber,string name, string surename, string password,string username)
        {
            this.id = id;
            this.mail = mail;
            this.phoneNumber = phoneNumber;
            this.name = name;
            this.surename = surename;
            this.password = password;
            this.username = username;
        }
    }
}
