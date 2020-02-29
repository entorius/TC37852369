using System;
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
            string firebasefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\ticketbase-36d66-firebase-adminsdk-50vwt-f21d8b1827.json";

            //string firebasefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\ticket-maindb-firebase-adminsdk-pqjyq-d3e72218f9.json";

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebasefileDirectory);

            
            
        }
        public static void setGoogleCloudEnvironmentVariable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string cloudstoragefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\ticket-base-test-6a936ec44f08.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", cloudstoragefileDirectory);

            //string cloudstoragefileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\ticket-main-268400-0f4758658acc.json";
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", cloudstoragefileDirectory);

        }
        public static string getGoogleCloudEnvironmentVariable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.FullName + @"\ticket-base-test-6a936ec44f08.json";

            //return Directory.GetParent(workingDirectory).Parent.FullName + @"\ticket-main-268400-0f4758658acc.json";

        }
    }
}
