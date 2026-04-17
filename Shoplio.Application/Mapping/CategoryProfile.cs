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
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            //Entity to Response DTo
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest=>dest.Description,opt=>opt.MapFrom(src=>src.Description));

            //DTo to Entity
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest=>dest.Description,opt=>opt.MapFrom(src=>src.Description));

            //Updata DTo to Entity
            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
