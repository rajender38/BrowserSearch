
namespace SearchEngine.RequestInput
{
    public class SearchInput
    {
        public string searchText { get; set; }
        public string findURL { get; set; }
        public string searchEngineTypes { get; set; }

        public SearchInput(string searchText, string findURL, string searchEngineTypes)
        {
            this.searchText = searchText;
            this.findURL = findURL;
            this.searchEngineTypes = searchEngineTypes;
        }
    }
}
