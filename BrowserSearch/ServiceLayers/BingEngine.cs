using System;
using System.Text;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.Properties;
using SearchEngine.RequestInput;

namespace SearchEngine.ServiceLayers
{
    public class BingEngine : ISearchEngine
    {
        IWebPost iwebPost;
        IRegExHtmlString iregExHtmlString;
        private readonly ILogger<BingEngine> ilogger;
        public SearchInput searchInput { get; set; }
        public BingEngine(IWebPost iwebPost, IRegExHtmlString iregExHtmlString, ILogger<BingEngine> ilogger)
        {
            this.iwebPost = iwebPost;
            this.iregExHtmlString = iregExHtmlString;
            this.ilogger = ilogger;
        }

        public string ScrapBrowser()
        {
            try
            {
                var searchEngineUrl = Resources.Bing;
                var maxItems = Convert.ToInt32(Resources.StaticPages);
                var matchResultCount = 0;
                var searchResult = new StringBuilder();
                for (int pageSize = 1; pageSize <= maxItems; pageSize = pageSize + 1)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, pageSize.ToString("D2"));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl, searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<li class=""b_algo"">(.*?)</li>", matchResultCount, searchResult);
                    matchResultCount = tupleResult.Item2;
                    searchResult = tupleResult.Item1;
                }
                return searchResult.ToString().TrimEnd(',');
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                throw;
            }
        }

    }
}
