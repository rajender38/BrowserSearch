using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Common
{
    public static class PadStart
    {
        public static string process(int pageSize)
        {
            if (pageSize <= 9)
                return "0" + Convert.ToString(pageSize);
            else
                return Convert.ToString(pageSize);
        }
    }
}
