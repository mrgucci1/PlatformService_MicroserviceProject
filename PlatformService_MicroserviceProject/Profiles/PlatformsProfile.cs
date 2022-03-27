using AutoMapper;
using PlatformService.Dtos;
using PlatformService_MicroserviceProject.Dtos;
using PlatformService_MicroserviceProject.Models;

namespace PlatformService_MicroserviceProject.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // When reading, source is platform and read dto is target
            CreateMap<Platform, PlatformReadDto>();
            // When creating, Source is create Dto and target is platform
            CreateMap<PlatformCreateDtos, Platform>();
            //
            CreateMap<PlatformReadDto, PlatformPublishDto>();
        }
    }
}
