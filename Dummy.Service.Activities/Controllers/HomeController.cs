using Microsoft.AspNetCore.Mvc;

namespace Dummy.Service.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Responding");
    }
}
