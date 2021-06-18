using Blog.Application.DTO;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.PostRatingDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Queries
{
    public class GetUserQuery : IGetUserQuery
    {
        private readonly BlogContext _context;

        public GetUserQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 24;

        public string Name => "Get one User";

        public GetUserDTO Execute(int search)
        {
            var query = _context.Users.Include(x=> x.Comments).ThenInclude(x=>x.PostComments).ThenInclude(x=>x.Post).Include(x=>x.PostRatings).ThenInclude(x=>x.Post).ThenInclude(x=>x.User).Include(x=>x.Posts).FirstOrDefault(x=> x.Id == search);

            if (query == null)
                throw new EntityNotFoundException(search, typeof(User));

            return new GetUserDTO
            {
                Id = query.Id,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Email = query.Email,
                Username = query.Username,
                Comments = query.Comments.Select(x => new GetCommentDTO {

                    Id = x.Id,
                    Body = x.Body,
                    CreatedAt = x.CreatedAt,
                    Heading = x.Heading,
                    Post = x.PostComments.Select(x => new GetPostDTO {

                        Id = x.Post.Id,
                        Heading = x.Post.Heading,
                        CreatedAt = x.Post.CreatedAt,
                        Image = x.Post.Image,
                        ReadTime = x.Post.ReadTime,
                        Text = x.Post.Text
                    }).FirstOrDefault()
                }),
                Posts=query.Posts.Select(x=> new GetPostDTO { 
                
                Id=x.Id,
                Heading=x.Heading,
                CreatedAt=x.CreatedAt,
                Image=x.Image,
                ReadTime=x.ReadTime,
                Text=x.Text
                }),
                Ratings= query.PostRatings.Select(x=> new GetPostRatingDTO { 
                PostId=x.PostId,
                UserId=x.UserId,
                Post = new GetPostDTO
                {
                    Id=x.Post.Id,
                    Heading = x.Post.Heading,
                    CreatedAt = x.Post.CreatedAt,
                    Image = x.Post.Image,
                     ReadTime= x.Post.ReadTime,
                     Text = x.Post.Text,


                },
                Rating=x.Rating,
                User = new UserDTO
                {
                    Id=x.User.Id,
                    FirstName=x.User.FirstName,
                    LastName=x.User.LastName,
                    Email=x.User.Email,
                    Username=x.User.Username
                }
                

                })
            };
        }
    }
}
