using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngineGuide
{
    public static class IdCardUtil
    {
        public static int GetAgeByIdCard(this string idCard)
        {
            int age = 0;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                }
                else if (idCard.Length == 15)
                {
                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
                }
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                age = ts.Days / 365;
            }
            return age;
        }

    }
}
