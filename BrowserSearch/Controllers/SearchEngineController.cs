using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngine.Helpers;
using SearchEngine.Interfaces;
using SearchEngine.RequestInput;

namespace SearchEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ILogger ilogger;
        IWebPost iwebPost;
        IregExHtmlString iregExHtmlString;

        public SearchEngineController(ILogger<SearchEngineController> logger, 
            IWebPost _iwebPost, 
            IregExHtmlString _iregExHtmlString)
        {
            ilogger = logger;
            iwebPost = _iwebPost;
            iregExHtmlString = _iregExHtmlString;
        }

        [HttpGet]
        public async Task<Response> RequestGet()
        {
            try
            {
                Response response = new Response();
                var searchText = await ReadRequest();
                string result = new Helpers.ProcessSearchEngineRequest(searchText).Process(iwebPost, iregExHtmlString);
                return new Response() { output = result==""?"No Results found":result };
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, "Error occured, please contact administratior");
                return new Response() { Error = "Error occured, please contact administratior" };
            }

            
        }
        
        private async Task<SearchInput> ReadRequest()
        {
            try
            {

                NameValueCollection body = new NameValueCollection();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString)))
                {
                    body = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                }
                else if (Request.Body != null)
                {
                    using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                    {
                        string s = await reader.ReadToEndAsync();
                        body = HttpUtility.ParseQueryString(s);
                    }

                }

                SearchInput values = new SearchInput(
                    body["searchText"] ?? string.Empty,
                    body["findURL"] ?? string.Empty, 
                    body["searchEngineTypes"] ?? string.Empty,
                    body["staticPages"]??"true")
                    ;
                return values;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }



    public class Response
    {
        public string output { get; set; }
        public string Error { get; set; }

    }


}
