﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PlatformsController> _logger;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ILogger<PlatformsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPlatforms")]
        public ActionResult<PlatformReadDto> GetAllPlatforms()
        {
            _logger.LogInformation("Getting all platforms");
            var platformItem = _repository.GetAllPlatforms();
            if (platformItem != null)
            {
                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
            }
            else
            {
                _logger.LogInformation("no platforms");

                return NoContent();
            }
        }

        [HttpGet("{id}", Name = "GetPlatformById") ]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        { 
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null)
            {
                _logger.LogInformation($"Found {platformItem.Id}");
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            else
            {
                _logger.LogInformation("platform not found.");
            }
            return NotFound();  
        }

        [HttpPost(Name = "CreatePlatform")]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { platformReadDto.Id }, platformReadDto);
        }

        [HttpDelete( "{id}", Name = "DeletePlatformById" )]
        public ActionResult DeletePlatformById( int id )
        {
            var platformItem = _repository.DeletePlatformById( id );
            if ( platformItem  )
            {
                _logger.LogInformation( $"Found {id} &deleted" );
                return NoContent( );
            }
            else
            {
                _logger.LogInformation($"{id}platform not found.");
            }
            return NotFound( );
        }
    }
}
