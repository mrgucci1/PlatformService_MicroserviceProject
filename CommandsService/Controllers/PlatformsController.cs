using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }
        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("Inbound POST # Command service");
            return Ok("Inbound test okay from Platforms Controller");
        }
    }
}
