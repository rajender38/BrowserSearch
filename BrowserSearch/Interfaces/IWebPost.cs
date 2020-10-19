using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Interfaces
{
    public interface IWebPost
    {
        public string GetHtmlResponse(string url,ISearchInput searchInput);
    }
}
