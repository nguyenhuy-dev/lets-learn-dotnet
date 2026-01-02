using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Helpers
{
    public class StringHelper
    {
        // Filter string without ""
        public static string FilterStringAgain(string str)
        {
            string value = "\"";
            if (str.StartsWith(value))
                return str.Substring(1, str.Length - 2);

            return str;
        }
    }
}
