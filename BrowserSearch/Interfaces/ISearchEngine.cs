using SearchEngine.RequestInput;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngine
    {
        public SearchInput searchInput { get; set; }
        public string ScrapBrowser();
    }
}
