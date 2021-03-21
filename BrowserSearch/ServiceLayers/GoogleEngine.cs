using System;
using System.Text;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.Properties;
using SearchEngine.RequestInput;

namespace SearchEngine.ServiceLayers
{
    public class GoogleEngine : ISearchEngine
    {
        IWebPost iwebPost;
        IRegExHtmlString iregExHtmlString;
        public SearchInput searchInput { get; set; }
        private readonly ILogger<GoogleEngine> ilogger;
        public GoogleEngine(IWebPost _iwebPost, IRegExHtmlString _iregExHtmlString, ILogger<GoogleEngine> ilogger)
        {
            iwebPost = _iwebPost;
            iregExHtmlString = _iregExHtmlString;
            this.ilogger = ilogger;
        }

        public string ScrapBrowser()
        {
            try
            {
                var searchEngineUrl = Resources.Google;
                var maxItems = Convert.ToInt32(Resources.StaticPages);
                var matchResultCount = 0;
                var searchResult = new StringBuilder();
                for (int pageSize = 1; pageSize <= maxItems; pageSize = pageSize + 1)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, pageSize.ToString("D2"));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl, searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<div class=""g"">(.*?)<div>", matchResultCount, searchResult);
                    matchResultCount = tupleResult.Item2;
                    searchResult = tupleResult.Item1;
                }
                return searchResult.ToString();
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                throw;
            }
        }

    }
}
