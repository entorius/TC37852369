using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public static class StringHelper
    {
        public static bool containsIllegalPathCharacters(string fileName)
        {
            char[] illegalCharacters = Path.GetInvalidFileNameChars();
            if(fileName.IndexOfAny(illegalCharacters) >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
