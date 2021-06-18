using AutoMapper;
using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.DTO.CommentDTOs;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Profiles
{
   public  class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>().ForMember(d => d.Id, opt => opt.MapFrom(p => p.Id))
                                          .ForMember(d => d.Text, opt => opt.MapFrom(p => p.Text))
                                          .ForMember(d => d.Heading, opt => opt.MapFrom(p => p.Heading))
                                          .ForMember(d => d.ReadTime, opt => opt.MapFrom(p => p.ReadTime))
                                          .ForMember(d => d.Text, opt => opt.MapFrom(p => p.Text))
                                          .ForMember(d => d.Image, opt => opt.MapFrom(p => p.Image))
                                        .ForMember(d => d.CreatedAt, opt => opt.MapFrom(p => p.CreatedAt))
                                          .ForMember(d => d.AverageRating, opt => opt.MapFrom(p => p.PostRatings.Select(x => x.Rating).Average()))
                                          .ForMember(d => d.Ratings, opt => opt.MapFrom(p => p.PostRatings.Select(x => x.Rating)))
                                          .ForMember(d => d.Categories, opt => opt.MapFrom(p => p.PostCategories.Select(y => new CategoryDTO
                                          {
                                              Id = y.Category.Id,
                                              Name = y.Category.Name

                                          })))
                                          .ForMember(d => d.Comments, opt => opt.MapFrom(p => p.PostComments.Select(y => new CommentDTO
                                          {

                                              Heading = y.Comment.Heading,
                                              Body = y.Comment.Body,
                                              CreatedAt = y.CreatedAt,
                                              Id = y.Comment.Id,
                                              User = new UserDTO
                                              {
                                                  FirstName = y.Comment.User.FirstName,
                                                  LastName = y.Comment.User.LastName,
                                                  Id = y.Comment.User.Id,
                                                  Email = y.Comment.User.Email,
                                                  Username = y.Comment.User.Username,


                                              }
                                          })));







        }
    }
}
