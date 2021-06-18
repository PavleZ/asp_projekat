using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Queries;
using Blog.Application.Searches;
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
    public class GetCommentsQuery : IGetCommentsQuery
    {
        private readonly BlogContext _context;

        public GetCommentsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 15;

        public string Name => "Comment search.";

        public PagedResponse<GetCommentDTO> Execute(CommentSearch search)
        {
            var query = _context.Comments.Include(x=>x.PostComments).ThenInclude(x=>x.Post).Include(x=>x.User).AsQueryable();
            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Heading.ToLower().Contains(search.Keyword) || x.Body.ToLower().Contains(search.Keyword));
            }


            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetCommentDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetCommentDTO
                {
                    Id=x.Id,
                    Body=x.Body,
                    CreatedAt=x.CreatedAt,
                    Heading=x.Heading,
                    Post=x.PostComments.Select(y=> new GetPostDTO {
                        Id = y.Post.Id,
                        Heading = y.Post.Heading,
                        CreatedAt = y.Post.CreatedAt,
                        Image = y.Post.Image,
                        ReadTime = y.Post.ReadTime,
                        Text = y.Post.Text
                    }).FirstOrDefault(),
                    User = new UserDTO {
                        Id = x.User.Id,
                        Email = x.User.Email,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Username = x.User.Username
                    }
                }).ToList()
            };
            return response;


          



        }
    }
}
