using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Interfaces
{
    public interface ISearchInput
    {
        public string searchText { get; set; }
        public string findURL { get; set; }
        public string searchEngineTypes { get; set; }
        public string staticPages { get; set; }

    }
}
