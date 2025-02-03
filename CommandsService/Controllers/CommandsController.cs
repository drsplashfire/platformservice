using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System;
using CommandsService.Models;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly IcommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(IcommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommandsForPlatform(int platformId) 
        {
            Console.WriteLine($"-->  Hit GetAllCommandsForPlatform {platformId}");

            if ( !_repository.PlatformExists(platformId) )
            {
                return NotFound();
            }
            else {

                var commands = _repository.GetCommandsForPlatform(platformId);
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
            }
        }

        [HttpGet("{CommandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int CommandId)
        {
            Console.WriteLine($"--> hit GetCommandForPlatform {platformId}/{CommandId}");
            if(!_repository.PlatformExists(platformId)) {
                return NotFound();
            }
            
            var command = _repository.GetCommand(platformId, CommandId);
            if ( command == null )
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
        }
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(CommandCreateDto commandCreateDto, int platformId)
        {
            Console.WriteLine($"--> hit CreateCommandForPlatform {platformId}");
            if ( !_repository.PlatformExists(platformId) )
            {
                return NotFound();
            }
            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(command, platformId);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId= platformId, commandId = commandReadDto.Id }, commandReadDto);

        }
    }
}
