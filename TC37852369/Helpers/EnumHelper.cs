using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public class EnumHelper
    {
        public int GetStringEnumIndex(Type enumType, string value)
        {
            int index = -1;
            try
            {
                foreach (var e in Enum.GetValues(enumType))
                {
                    if (e.ToString().Equals(value))
                    {
                        index = (int)e;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return index;
        }
    }
}
