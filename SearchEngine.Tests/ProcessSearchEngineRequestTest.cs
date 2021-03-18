using System;
using Xunit;
using SearchEngine.RequestInput;
using SearchEngine.Interfaces;
using BrowserSearch;
using Microsoft.Extensions.DependencyInjection;

namespace SearchEngine.Tests
{
    public class ProcessSearchEngineRequestTest
    {

        readonly IServiceProvider _services =
        Program.CreateHostBuilder(new string[] { }).Build().Services;


        [Fact]
        public void TestSearchGoogle()
        {
            var searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "GoogleEngine");
            var iProcessSearchEngineRequest = _services.GetRequiredService<IProcessSearchEngineRequest>();
            var sut = iProcessSearchEngineRequest.Process(searchInput);
            Assert.Equal("1, 11, 21, 31, 41, 51, 61, 71, 81, 91, ", sut.ToString());
        }

        [Fact]
        public void TestSearchbing()
        {
            var searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "BingEngine");
            var iProcessSearchEngineRequest = _services.GetRequiredService<IProcessSearchEngineRequest>();
            var sut = iProcessSearchEngineRequest.Process(searchInput);
            Assert.Equal("", sut.ToString());
        }

    }
}
