using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController
    {
        [HttpGet]
        public string Test()
        {
            return "tammam";
        }
    }
}