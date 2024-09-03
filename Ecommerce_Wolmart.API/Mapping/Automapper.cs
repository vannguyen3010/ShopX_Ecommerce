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

            //CreateMap<CreateCateProductDto, CateProduct>();

            //CreateMap<CateProduct, CategoryDto>();

            CreateMap<Banner, BannerDto>();

            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, ContactDto>();

            CreateMap<UpdateContactDto, Contact>();

            CreateMap<CreateCateProductDto, CateProduct>()
              .ForMember(dest => dest.File, opt => opt.Ignore());

            CreateMap<CateProduct, CateProductDto>();
        }
    }
}
