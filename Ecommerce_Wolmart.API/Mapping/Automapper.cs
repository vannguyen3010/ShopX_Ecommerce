using AutoMapper;
using Entities.Identity;
using Entities.Models;
using Shared.DTO.Banner;
using Shared.DTO.BannerProduct;
using Shared.DTO.Category;
using Shared.DTO.CommentProduct;
using Shared.DTO.Contact;
using Shared.DTO.Product;
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

            CreateMap<Banner, BannerDto>();

            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, ContactDto>();

            CreateMap<UpdateContactDto, Contact>();

            CreateMap<CreateCateProductDto, CateProduct>()
              .ForMember(dest => dest.File, opt => opt.Ignore());

            CreateMap<CateProduct, CateProductDto>();

            CreateMap<CreateProductDto, Product>();

            CreateMap<Product, ProductDto>();

            CreateMap<CreateCommentProductDto, CommentProduct>();

            CreateMap<CommentProduct, CommentProductDto>();

            CreateMap<CreateBannerProductDto, BannerProduct>();
            CreateMap<BannerProduct, BannerProductDto>();
        }
    }
}
