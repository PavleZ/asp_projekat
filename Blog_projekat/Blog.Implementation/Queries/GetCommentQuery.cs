using Blog.Application.DTO;
using Blog.Application.DTO.PostDTOs;
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
    public class GetCommentQuery : IGetCommentQuery
    {
        private readonly BlogContext _context;

        public GetCommentQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 20;

        public string Name => "Get one comment";

        public GetCommentDTO Execute(int search)
        {
            var query = _context.Comments.Include(x=>x.PostComments).ThenInclude(x=>x.Post).Include(x=>x.User).FirstOrDefault(x=>x.Id == search);

            if (query == null)
                throw new EntityNotFoundException(search, typeof(Comment));
            return new GetCommentDTO
            {
                Id = query.Id,
                Body = query.Body,
                CreatedAt = query.CreatedAt,
                Heading = query.Heading,
                Post = query.PostComments.Select(x =>  new GetPostDTO
                {
                    Id = x.Post.Id,
                    Heading = x.Post.Heading,
                    CreatedAt = x.Post.CreatedAt,
                    Image = x.Post.Image,
                    ReadTime = x.Post.ReadTime,
                    Text=x.Post.Text
                }).FirstOrDefault(),
                User= new UserDTO
                {
                    Id= query.User.Id,
                    Email=query.User.Email,
                    FirstName=query.User.FirstName,
                    LastName = query.User.LastName,
                    Username=query.User.Username
                    
                }
               

            };


        }
    }
}
