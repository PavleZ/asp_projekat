using AutoMapper;
using Blog.Application.DTO;
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
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, GetCommentDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Body, opt => opt.MapFrom(c => c.Body))
                .ForMember(d => d.Heading, opt => opt.MapFrom(c => c.Heading))
                .ForMember(d => d.Post, opt => opt.MapFrom(c => c.PostComments.Select(y => new GetPostDTO {
                    Id = y.Post.Id,
                    Heading = y.Post.Heading,
                    CreatedAt = y.Post.CreatedAt,
                    Image = y.Post.Image,
                    ReadTime = y.Post.ReadTime,
                    Text = y.Post.Text

                })))
                .ForMember(d => d.User , opt => opt.MapFrom(c => new UserDTO { 
                
                Id=c.User.Id,
                Email=c.User.Email,
                FirstName= c.User.FirstName,
                LastName= c.User.LastName,
                Username= c.User.Username

                }));









        }

    }
}
