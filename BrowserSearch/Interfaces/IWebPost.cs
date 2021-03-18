using SearchEngine.RequestInput;

namespace SearchEngine.Interfaces
{
    public interface IWebPost
    {
        public string GetHtmlResponse(string url, SearchInput searchInput);
    }
}
