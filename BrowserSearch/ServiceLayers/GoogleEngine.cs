using System;
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
                var increment = 0;
                var result = string.Empty;
                for (int pageSize = 1; pageSize <= maxItems; pageSize = pageSize + 1)
                {
                    searchEngineUrl = string.Format(searchEngineUrl, pageSize.ToString("D2"));
                    var htmpString = iwebPost.GetHtmlResponse(searchEngineUrl, searchInput);
                    var tupleResult = iregExHtmlString.FindTextAndGetResult(htmpString, searchInput.findURL, @"(?is)<div class=""g"">(.*?)<div>", increment, result);
                    increment = tupleResult.Item2;
                    result = tupleResult.Item1;
                }
                return result.TrimEnd(',');
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                throw;
            }
        }

    }
}
