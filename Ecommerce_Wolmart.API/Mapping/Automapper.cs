using AutoMapper;
using Entities.Identity;
using Entities.Models;
using Shared.DTO.Banner;
using Shared.DTO.Category;
using Shared.DTO.Contact;
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

            CreateMap<CreateBannerDto, Banner>();

            CreateMap<CreateCategoryDto, Category>();

            CreateMap<Category, CategoryDto>();

            CreateMap<Banner, BannerDto>();

            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, ContactDto>();

            CreateMap<UpdateContactDto, Contact>();
        }
    }
}
