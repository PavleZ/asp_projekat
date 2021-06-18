using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.PostRatingDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Blog.Implementation.Profiles
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<User, GetUserDTO>().
                    ForMember(d => d.Id, opt => opt.MapFrom(u => u.Id)).
                    ForMember(d => d.FirstName, opt => opt.MapFrom(u => u.FirstName)).
                    ForMember(d => d.LastName, opt => opt.MapFrom(u => u.LastName)).
                    ForMember(d => d.Username, opt => opt.MapFrom(u => u.Username)).
                    ForMember(d => d.Email, opt => opt.MapFrom(u => u.Email)).
                    ForMember(d => d.Comments, opt => opt.MapFrom(u => u.Comments.Select(x => new GetCommentDTO
                    {
                        Id = x.Id,
                        Body = x.Body,
                        Heading = x.Heading,
                        CreatedAt = x.CreatedAt,
                        Post = x.PostComments.Select(y => new GetPostDTO
                        {
                            Id = y.Post.Id,
                            CreatedAt = y.Post.CreatedAt,
                            Image = y.Post.Image,
                            Heading = y.Post.Heading,
                            ReadTime = y.Post.ReadTime,
                            Text = y.Post.Text

                        }).First(),
                        User = new UserDTO
                        {
                            Id = x.User.Id,
                            Email = x.User.Email,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Username = x.User.Username
                        }
                    }))).
                    ForMember(d => d.Posts, opt => opt.MapFrom(u => u.Posts.Select(x => new GetPostDTO
                    {

                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        Heading = x.Heading,
                        Image = x.Image,
                        ReadTime = x.ReadTime,
                        Text = x.Text


                    }))).
                    ForMember(d => d.Ratings, opt => opt.MapFrom(u => u.PostRatings.Select(x => new GetPostRatingDTO
                    {
                        Post = new GetPostDTO
                        {
                            Id = x.Post.Id,
                            Text = x.Post.Text,
                            CreatedAt = x.Post.CreatedAt,
                            Heading = x.Post.Heading,
                            Image = x.Post.Image,
                            ReadTime = x.Post.ReadTime
                        },
                        PostId = x.Post.Id,
                        Rating = x.Rating,
                        UserId = x.User.Id,
                        User = new UserDTO
                        {
                            Id = x.User.Id,
                            Email = x.User.Email,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Username = x.User.Username


                        }

                    })));







        }
    }
}
