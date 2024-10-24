﻿using AutoMapper;
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
using Shared.DTO.Checkout;
using Shared.DTO.CommentProduct;
using Shared.DTO.Contact;
using Shared.DTO.ImageProfile;
using Shared.DTO.Introduce;
using Shared.DTO.Order;
using Shared.DTO.Payment;
using Shared.DTO.Product;
using Shared.DTO.ShippingCost;
using Shared.DTO.SocialMediaInfo;
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

            CreateMap<BannerUpdateDto, Banner>();

            CreateMap<Banner, BannerDto>();

            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, ContactDto>();

            CreateMap<UpdateContactDto, Contact>();

            CreateMap<CreateCateProductDto, CateProduct>()
              .ForMember(dest => dest.File, opt => opt.Ignore());

            CreateMap<CateProduct, CateProductDto>()
              .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom(src => src.ParentCategory))
              .ForMember(dest => dest.CategoriesObjs, opt => opt.MapFrom(src => src.CategoriesObjs));

            CreateMap<CreateProductDto, Product>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore());

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

            CreateMap<CategoryIntroduce, CategoryIntroduceDto>()
                .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Introduces.Count()));

            CreateMap<UpdateCateIntroDto, CategoryIntroduceDto>();

            CreateMap<Introduce, IntroduceDto>();

            CreateMap<CreateAddressDto, Address>();

            CreateMap<Address, AddressDto>();

            CreateMap<UpdateAddressDto, Address>();

            CreateMap<CreateProfileUserDto, ProfileUser>();

            CreateMap<ProfileUser, ProfileUserDto>();

            CreateMap<UpdateProFileDto, ProfileUser>();


            CreateMap<Product, CartItemDto>();

            CreateMap<CartItemDto, CartItem>();

            CreateMap<AddToCartDto, CartItem>();

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => (src.Price * src.Quantity) - (src.Discount * src.Quantity)));

            CreateMap<UpdateCartItemDto, CartItem>();

            CreateMap<CreatePaymentDto, PaymentMethod>();

            CreateMap<PaymentMethod, PaymentMethodDto>();

            CreateMap<UpdatePaymentDto, PaymentMethod>();

            CreateMap<ShippingCost, ShippingCostDto>();

            CreateMap<UpdateCostDto, ShippingCost>();

            CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderItemDto, OrderItem>();

            // Ánh xạ từ Order -> OrderDto
            CreateMap<Order, OrderDto>();

            CreateMap<OrderDto, Order>();

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, CartItemDto>();

            CreateMap<Order, CartItemDto>();

            CreateMap<CartItemDto, Order>();

            CreateMap<Order, CheckoutDto>();

            CreateMap<Checkout, CheckoutDto>();

            CreateMap<UpdateCheckoutDto, Checkout>();

            CreateMap<CreateSocialMediaInfoDto, SocialMediaInfo>();

            CreateMap<SocialMediaInfo, SocialMediaInfoDto>();

            CreateMap<UpdateSocialMediaInfoDto, SocialMediaInfo>();

        }
    }
}
