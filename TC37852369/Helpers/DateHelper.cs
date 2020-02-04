using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public static class DateHelper
    {
        public static DateTime getToday()
        {

            return DateHelper.setDateToMidnight(DateTime.Now);
        }
        public static DateTime setDateToMidnight(DateTime time)
        {
            return new DateTime(
                time.Year,
                time.Month,
                time.Day,
                0,
                0,
                0);
        }
    }
}
