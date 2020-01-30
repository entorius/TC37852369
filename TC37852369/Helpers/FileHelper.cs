using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public class FileHelper
    {
        public void DeleteAllFilesFormDirectory(string directory)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(directory);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
