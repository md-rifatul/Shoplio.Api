using AutoMapper;
using Shoplio.Application.DTOs;
using Shoplio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>()
                .ForMember(dest=>dest.CartItemDtos,opt=>opt.MapFrom(src=>src.CartItems));
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity))
                .ForMember(dest => dest.Images,opt=>opt.MapFrom(src=>src.Product.Images.Select(img=>img.ImageUrl).ToList()));
                
        }
    }
}
