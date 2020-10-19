using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Interfaces
{
    public interface IregExHtmlString
    {
        public Tuple<string, int> FindTextAndGetResult(string htmlString, string findUrl, string regExSearch, int increment, string result);
    }
}
