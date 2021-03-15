using AutoMapper;
using Laborlance_API.Dtos;
using Laborlance_API.Models;

namespace Laborlance_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>(); 
        }
    }
}