using AutoMapper;
using Entities.Identity;
using Shared.DTO.User;

namespace Ecommerce_Wolmart.API.Mapping
{
    public class Automapper : Profile
    {
        public Automapper() 
        {
            CreateMap<RegisterDto, User>();

            CreateMap<User, UserDto>();

            CreateMap<UpdateUserDto, User>();
        }
    }
}
