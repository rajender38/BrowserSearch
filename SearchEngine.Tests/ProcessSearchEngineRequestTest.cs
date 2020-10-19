using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SearchEngine.Helpers;
using SearchEngine.RequestInput;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SearchEngine.Interfaces;
using SearchEngine.Controllers;
using Moq;

namespace SearchEngine.Tests
{
    public class ProcessSearchEngineRequestTest
    {
        ProcessSearchEngineRequest processSearchEngineRequest;
        private SearchInput searchInput;
        public ILogger ilogger;
        private readonly Mock<ILogger<SearchEngineController>> _mockLogger;
        IWebPost iwebPost;
        IregExHtmlString iregExHtmlString;
        public ProcessSearchEngineRequestTest()
        {
            //ilogger = new Logger<SearchEngineController>();
            iwebPost = new WebPost();
            iregExHtmlString = new RegExHtmlString();
        }

        [Fact]
        public void TestWithActualBrowserSearchGoogle()
        {

            var mock = new Mock<ILogger<SearchEngineController>>();
            ILogger<SearchEngineController> logger = mock.Object;
            this.searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "google",
                "false");
            processSearchEngineRequest = new ProcessSearchEngineRequest(searchInput);
            var sut = processSearchEngineRequest.Process(iwebPost, iregExHtmlString);
            Assert.Equal("4, 16, 28, 40, 52, 64, ", sut);
        }

        [Fact]
        public void TestWithStaticPageBrowserSearchGoogle()
        {

            var mock = new Mock<ILogger<SearchEngineController>>();
            ILogger<SearchEngineController> logger = mock.Object;
            this.searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "google",
                "true");
            processSearchEngineRequest = new ProcessSearchEngineRequest(searchInput);
            var sut = processSearchEngineRequest.Process(iwebPost, iregExHtmlString);
            Assert.Equal("1, 11, 21, 31, 41, 51, 61, 71, 81, 91, ", sut);
        }

        [Fact]
        public void TestWithActualBrowserSearchBing()
        {

            var mock = new Mock<ILogger<SearchEngineController>>();
            ILogger<SearchEngineController> logger = mock.Object;
            this.searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "bing",
                "false");
            processSearchEngineRequest = new ProcessSearchEngineRequest(searchInput);
            var sut = processSearchEngineRequest.Process(iwebPost, iregExHtmlString);
            Assert.True(!string.IsNullOrEmpty(sut)); // Resukt is dynamic
        }

        [Fact]
        public void TestWithStaticPageBrowserSearchbing()
        {

            var mock = new Mock<ILogger<SearchEngineController>>();
            ILogger<SearchEngineController> logger = mock.Object;
            this.searchInput = new SearchInput(
                "online title search",
                "https://www.infotrack.com.au",
                "bing",
                "true");
            processSearchEngineRequest = new ProcessSearchEngineRequest(searchInput);
            var sut = processSearchEngineRequest.Process(iwebPost, iregExHtmlString);
            Assert.Equal("", sut);
        }
    }
}
