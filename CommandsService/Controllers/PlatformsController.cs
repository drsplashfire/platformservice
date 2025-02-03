using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;
        private readonly IcommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(
            IcommandRepo repository,
            IMapper mapper,
            ILogger<PlatformsController> logger)
        {
            _logger= logger;
            _repository= repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms() 
        {
            Console.WriteLine(" --> getting platforms");
            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
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
