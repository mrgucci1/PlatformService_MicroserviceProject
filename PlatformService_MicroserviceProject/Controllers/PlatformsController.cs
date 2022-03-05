using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService_MicroserviceProject.Data;
using PlatformService_MicroserviceProject.Dtos;
using PlatformService_MicroserviceProject.Models;

namespace PlatformService_MicroserviceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;

        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("Getting Platforms");
            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("Getting Platform by id");
            var platformItem = _repository.GetPlatformById(id);
            if(platformItem == null)
                return NotFound();
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDtos platformCreateDtos)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDtos);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            //respone with code 201, say what request can be used to GET created platform, then respond with id and plaformreaddto of created object
            //big part of REST
            return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDto.Id }, platformReadDto);
        }
    }
}
