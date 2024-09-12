using AutoMapper;
using Entities.Identity;
using Entities.Models;
using Entities.Models.Address;
using Shared.DTO.Address;
using Shared.DTO.Banner;
using Shared.DTO.BannerProduct;
using Shared.DTO.Cart;
using Shared.DTO.Category;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.CommentProduct;
using Shared.DTO.Contact;
using Shared.DTO.ImageProfile;
using Shared.DTO.Introduce;
using Shared.DTO.Payment;
using Shared.DTO.Product;
using Shared.DTO.ShippingCost;
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

            //CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages));

            CreateMap<UpdateProductDto, Product>()
               .ForMember(dest => dest.ImageFilePath, opt => opt.Ignore())
               .ForMember(dest => dest.ProductImages, opt => opt.Ignore());

            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<ProductImageDto, ProductImage>();

            CreateMap<CreateCommentProductDto, CommentProduct>();

            CreateMap<CommentProduct, CommentProductDto>();

            CreateMap<CreateBannerProductDto, BannerProduct>();

            CreateMap<BannerProduct, BannerProductDto>();

            CreateMap<UpdateBannerProductDto, BannerProduct>();

            CreateMap<CreateIntroduceDto, Introduce>();

            CreateMap<UpdateIntroduceDto, Introduce>();

            CreateMap<CreateCategoryIntroDto, CategoryIntroduce>();

            CreateMap<CategoryIntroduce, CategoryIntroduceDto>();

            CreateMap<UpdateCateIntroDto, CategoryIntroduceDto>();

            CreateMap<Introduce, IntroduceDto>();

            CreateMap<CreateAddressDto, Address>();

            CreateMap<Address, AddressDto>();

            CreateMap<UpdateAddressDto, Address>();

            CreateMap<CreateImagePrifileDto, Image>();

            CreateMap<Image, ImageProfileDto>();

            CreateMap<UpdateImageProFileDto, Image>();

            CreateMap<Product, CartItemDto>();

            CreateMap<CartItemDto, CartItem>();

            CreateMap<AddToCartDto, CartItem>();

            CreateMap<CartItem, CartItemDto>();

            CreateMap<CartItem, CartItemDto>();

            CreateMap<UpdateCartItemDto, CartItem>();

            CreateMap<CreatePaymentDto, PaymentMethod>();

            CreateMap<PaymentMethod, PaymentMethodDto>();

            CreateMap<UpdatePaymentDto, PaymentMethod>();

            CreateMap<ShippingCost, ShippingCostDto>();

            CreateMap<UpdateCostDto, ShippingCost>();


        }
    }
}
