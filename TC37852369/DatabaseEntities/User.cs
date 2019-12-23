using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.DatabaseEntities
{
    public class User
    {
        public string id { get; set; }
        public string mail { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public void setUserData(string id,string mail, string name, string password,string username)
        {
            this.id = id;
            this.mail = mail;
            this.name = name;
            this.password = password;
            this.username = username;
        }
    }
}
