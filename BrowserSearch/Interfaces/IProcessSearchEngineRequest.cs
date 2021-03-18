using SearchEngine.RequestInput;

namespace SearchEngine.Interfaces
{
    public interface IProcessSearchEngineRequest
    {
        public string Process(SearchInput searchInput);
    }
}