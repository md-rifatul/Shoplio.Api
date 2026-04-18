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
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.ImageUrls,opt => opt.MapFrom(src =>
                    src.Images != null
                        ? src.Images.Select(i => i.ImageUrl ?? string.Empty)
                        : Enumerable.Empty<string>()));


            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    (src.ImageUrls ?? Enumerable.Empty<string>()).Select(url => new ProductImage
                    {
                        ImageUrl = url
                    })
                ));


            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
    }
}
