using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.RequestInput;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngine.Helpers
{

    public class ProcessSearchEngineRequest
    {
        private SearchInput searchInput;

        public ProcessSearchEngineRequest(SearchInput searchInput)
        {
            this.searchInput = searchInput;
        }

        public string Process(IWebPost iwebPost, IregExHtmlString iregExHtmlString)
        {
            try
            {
                List<string> searchEngineTypesList = GetListOfSearchEngineName(searchInput.searchEngineTypes ?? string.Empty);

                string result = string.Empty;
                foreach (string searchEngineType in searchEngineTypesList)
                {

                    if (searchEngineType.ToUpper() == "GOOGLE")
                        result = SearchEngine(new GoogleEngine(iwebPost, iregExHtmlString), searchInput, searchEngineType);
                    else if (searchEngineType.ToUpper() == "BING")
                        result = SearchEngine(new BingEngine(iwebPost, iregExHtmlString), searchInput, searchEngineType);

                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<string> GetListOfSearchEngineName(string searchEngineTypes) => searchEngineTypes.Split(',').ToList();
        private string SearchEngine(ISearchEngine iSearchEngine, SearchInput searchInput, string searchEngineType)
        {
            try
            {
                iSearchEngine.searchInput = searchInput;
                iSearchEngine.searchEngineName = searchEngineType;
                if (searchInput.staticPages == "true")
                    return iSearchEngine.ScrapBrowserStaticPage();
                else
                    return iSearchEngine.ScrapBrowser();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}