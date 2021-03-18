using Microsoft.AspNetCore.Mvc;

namespace BrowserSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "[This is test API]";
        }
    }
}
