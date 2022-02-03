using Microsoft.AspNetCore.Mvc;

namespace Snt.Romashka.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController: ControllerBase
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello World!";
        }
    }
}