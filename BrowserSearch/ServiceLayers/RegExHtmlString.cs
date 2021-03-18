using SearchEngine.Interfaces;
using System;
using System.Text.RegularExpressions;


namespace SearchEngine.ServiceLayers
{
    public class RegExHtmlString : IRegExHtmlString
    {

        public Tuple<string, int> FindTextAndGetResult(string htmlString, string findUrl, string regExSearch, int increment, string result)
        {
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            Regex regx = new Regex(@"(?is)<body(?:\s[^>]*)>(.*?)(?:</\s*body\s*>|</\s*html\s*>|$)", options);
            Match match = regx.Match(htmlString);
            if (match.Success)
            {
                string theBody = match.Value;
                Regex rg = new Regex(regExSearch, options);
                var matches = rg.Matches(theBody);
                if (matches.Count > 0)
                {
                    foreach (Match m in matches)
                    {
                        increment++;
                        if (m.Value.Contains(findUrl))
                        {
                            result += Convert.ToString(increment) + ", ";
                        }
                    }
                }
            }
            return new Tuple<string, int>(result, increment);
        }
    }
}
