using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;

        public PlatformsController(ILogger<PlatformsController> logger)
        {
            _logger= logger;
        }

        [HttpPost]
        public ActionResult TestInboundConnection( )
        {
            _logger.LogInformation("--> Inbound Post # CommandService");
            Console.WriteLine("--> Inbound Post # CommandService");

            return Ok("Inbound Test From PlatformsController");
        }
    }
}
