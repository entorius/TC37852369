using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            // If necessary, create it.
            if (value == null)
            {
                string workingDirectory = Environment.CurrentDirectory;
                string fileDirectory = Directory.GetParent(workingDirectory).Parent.FullName + @"\TicketBase-7013fb49b87b.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", fileDirectory);

                // Now retrieve it.
                value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
