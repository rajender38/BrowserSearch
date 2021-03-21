using System;
using System.Text;

namespace SearchEngine.Interfaces
{
    public interface IRegExHtmlString
    {
        public Tuple<StringBuilder, int> FindTextAndGetResult(string htmlString, string findUrl, string regExSearch, int matchResultCount, StringBuilder result);
    }
}
