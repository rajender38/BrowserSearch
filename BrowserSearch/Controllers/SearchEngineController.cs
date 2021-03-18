using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.RequestInput;

namespace SearchEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ILogger<SearchEngineController> ilogger;
        IProcessSearchEngineRequest iProcessSearchEngineRequest;

        public SearchEngineController(ILogger<SearchEngineController> ilogger,
             IProcessSearchEngineRequest iProcessSearchEngineRequest)
        {
            this.ilogger = ilogger;
            this.iProcessSearchEngineRequest = iProcessSearchEngineRequest;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SearchInput searchInput)
        {
            try
            {
                string result = await Task.FromResult(iProcessSearchEngineRequest.Process(searchInput));
                return Ok(result);
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                return BadRequest("Error occured, please contact administratior");
            }
        }
    }






}
