using SearchEngine.Interfaces;

namespace SearchEngine.RequestInput
{
    public class SearchInput : ISearchInput
    {
        public string searchText { get; set; }
        public string findURL { get; set; }
        public string searchEngineTypes { get; set; }
        public string staticPages { get; set; }

        public SearchInput(string searchText, string findURL, string searchEngineTypes, string staticPages)
        {
            this.searchText = searchText;
            this.findURL = findURL;
            this.searchEngineTypes = searchEngineTypes;
            this.staticPages = staticPages;
               
        }
    }
}
