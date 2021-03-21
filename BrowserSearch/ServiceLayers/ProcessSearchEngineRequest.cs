using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.RequestInput;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngine.ServiceLayers
{

    public class ProcessSearchEngineRequest : IProcessSearchEngineRequest
    {
        private readonly ILogger<ProcessSearchEngineRequest> ilogger;
        IEnumerable<ISearchEngine> iSearchEngine;
        public ProcessSearchEngineRequest(IEnumerable<ISearchEngine> iSearchEngine, ILogger<ProcessSearchEngineRequest> ilogger)
        {
            this.iSearchEngine = iSearchEngine;
            this.ilogger = ilogger;
        }

        private string[] GetListOfSearchEngineNames(string searchEngineTypes) => searchEngineTypes.Split(',').ToArray();

        public string Process(SearchInput searchInput)
        {
            try
            {
                string searchResult = string.Empty;
                var searchEngines = GetListOfSearchEngineNames(searchInput.searchEngineTypes ?? string.Empty);
                foreach (var searchEngine in iSearchEngine)
                {
                    if (searchEngines.Contains(searchEngine.GetType().Name))
                    {
                        searchEngine.searchInput = searchInput;
                        searchResult = searchEngine.ScrapBrowser();
                    }
                }
                return searchResult == string.Empty? "0": searchResult.Trim().TrimEnd(',');
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                throw;
            }
        }


    }
}