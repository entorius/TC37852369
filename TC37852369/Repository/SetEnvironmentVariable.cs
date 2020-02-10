﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Repository
{
    
    public static class SetEnvironmentVariable
    {
        public static void setFirestoreEnvironmentVariable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string firebasefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\TicketBase-7013fb49b87b.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebasefileDirectory);
        }
        public static void setGoogleCloudEnvironmentVariable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string cloudstoragefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\driven-realm-267109-5c7e2e9ddd6d.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", cloudstoragefileDirectory);
            Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"));
            var some = "";
        }
        public static string getGoogleCloudEnvironmentVariable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.FullName + @"\driven-realm-267109-5c7e2e9ddd6d.json";
        }
    }
}
