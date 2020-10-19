using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Interfaces
{
    public interface ISearchEngine
    {
        public ISearchInput searchInput { get; set; }
        public string searchEngineName { get; set; }
        public string ScrapBrowser();
        public string ScrapBrowserStaticPage();
    }
}
