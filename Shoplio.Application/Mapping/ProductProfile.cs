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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.Name))
                .ForMember(dest=>dest.ImageUrls,opt=>opt.Ignore());


            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest=>dest.Images,opt=>opt.Ignore());


            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
