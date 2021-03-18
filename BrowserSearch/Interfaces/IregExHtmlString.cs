using System;

namespace SearchEngine.Interfaces
{
    public interface IRegExHtmlString
    {
        public Tuple<string, int> FindTextAndGetResult(string htmlString, string findUrl, string regExSearch, int increment, string result);
    }
}
