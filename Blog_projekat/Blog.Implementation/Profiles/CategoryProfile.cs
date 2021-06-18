using AutoMapper;
using Blog.Application.DTO.CategoryDTOs;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Profiles
{
  public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {

            CreateMap<Category, CategoryDTO>().ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                                                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name));


            CreateMap<CategoryDTO, Category>().ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                                                 .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name));

        }

    }
}
