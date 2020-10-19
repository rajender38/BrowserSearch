using System;
using Microsoft.Extensions.Logging;
using SearchEngine.Common;
using SearchEngine.Interfaces;
using SearchEngine.Properties;
using SearchEngine.RequestInput;

namespace SearchEngine.Helpers
{
    public class GoogleEngine : ISearchEngine
    {
        IWebPost iwebPost;
        IregExHtmlString iregExHtmlString;
        public ISearchInput searchInput { get; set; }
        public string searchEngineName { get; set; }

        public GoogleEngine(IWebPost _iwebPost, IregExHtmlString _iregExHtmlString)
        {
            iwebPost = _iwebPost;
            iregExHtmlString = _iregExHtmlString;

        }

        public string ScrapBrowser()
        {
            try
            {
                string searchEngineUrl = Resources.Google;
                int maxItems = Convert.ToInt32(Resources.MaxItems);
                int increment = 0;
                string result = string.Empty;

                for (int pageSize = 0; pageSize <= maxItems; pageSize = pageSize + 10)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, searchInput.searchText, Convert.ToString(pageSize));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl,searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<div class=""ZINbbc xpd O9g5cc uUPGi"">(.*?)<div>", increment, result);
                    increment = tupleResult.Item2;
                    result = tupleResult.Item1;

                }
                return result.TrimEnd(',');
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ScrapBrowserStaticPage()
        {
            try
            {
                string searchEngineUrl = Resources.GoogleStaticPage;
                int maxItems = Convert.ToInt32(Resources.StaticPages);
                int increment = 0;
                string result = string.Empty;

                for (int pageSize = 1; pageSize <= maxItems; pageSize = pageSize + 1)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, PadStart.process(pageSize));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl,searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<div class=""g"">(.*?)<div>", increment, result);
                    increment = tupleResult.Item2;
                    result = tupleResult.Item1;

                }
                return result.TrimEnd(',');
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
