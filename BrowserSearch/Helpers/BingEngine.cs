using System;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.Properties;
using SearchEngine.Common;

namespace SearchEngine.Helpers
{
    public class BingEngine : ISearchEngine
    {
        IWebPost iwebPost;
        IregExHtmlString iregExHtmlString;
        public ISearchInput searchInput { get; set; }
        public BingEngine(IWebPost _iwebPost, IregExHtmlString _iregExHtmlString)
        {
            iwebPost = _iwebPost;
            iregExHtmlString = _iregExHtmlString;
        }

        public string searchEngineName { get; set; }


        public string ScrapBrowser()
        {
            try
            {
                string searchEngineUrl = Resources.Bing;
                int maxItems = Convert.ToInt32(Resources.MaxItems);
                int increment = 0;
                string result = string.Empty;

                for (int pageSize = 0; pageSize <= maxItems; pageSize = pageSize + 10)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, searchInput.searchText, Convert.ToString(pageSize));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl, searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<li class=""b_algo"">(.*?)</li>", increment, result);
                    increment = tupleResult.Item2;
                    result =  tupleResult.Item1;

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
                string searchEngineUrl = Resources.BingStaticPage;
                int maxItems = Convert.ToInt32(Resources.StaticPages);
                int increment = 0;
                string result = string.Empty;

                for (int pageSize = 1; pageSize <= maxItems; pageSize = pageSize + 1)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, PadStart.process(pageSize));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl,searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<li class=""b_algo"">(.*?)</li>", increment, result);
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
