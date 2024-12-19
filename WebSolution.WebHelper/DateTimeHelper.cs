using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebSolution.WebHelper
{
    public class DateTimeHelper
    {
        public static DateTime ObjectToDateTime(object time)
        {
            try
            {
                if (time != null)
                {
                    return DateTime.ParseExact(time.ToString(),
                                      "yyyyMMddHHmm",
                                       CultureInfo.InvariantCulture);
                }
                return new DateTime(2016, 1, 1);
            }
            catch (Exception ex)
            {
            }
            return new DateTime(2016, 1, 1);
        }
        public static DateTime Long8ToDateTime(long time)
        {
            try
            {
                if (time.ToString().Length == 8)
                {
                    return DateTime.ParseExact(time.ToString(),
                                      "yyyyMMdd",
                                       CultureInfo.InvariantCulture);
                }
                return new DateTime(2016, 1, 1);
            }
            catch (Exception ex)
            {
            }
            return new DateTime(2016, 1, 1);
        }
    }
}
