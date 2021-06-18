using AutoMapper;
using Blog.Application.DTO;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Queries;
using Blog.Application.Searches;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Queries
{
    public class GetUsersQuery : IGetUsersQuery
    {
        private readonly BlogContext _context;

        public GetUsersQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 18;

        public string Name => "Search users.";

        public PagedResponse<GetUserDTO> Execute(UserSearch search)
        {
            var query = _context.Users.AsQueryable();

            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.FirstName) || !string.IsNullOrWhiteSpace(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName));
            }
            if (!string.IsNullOrEmpty(search.LastName) || !string.IsNullOrWhiteSpace(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName));
            }

            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetUserDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetUserDTO
                {
                   Id=x.Id,
                   Email=x.Email,
                   FirstName=x.FirstName,
                   LastName=x.LastName,
                   Username=x.Username,
                   Comments= x.Comments.Select(x => new GetCommentDTO
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

                       }).FirstOrDefault(),
                       User = new UserDTO
                       {
                           Id = x.User.Id,
                           Email = x.User.Email,
                           FirstName = x.User.FirstName,
                           LastName = x.User.LastName,
                           Username = x.User.Username
                       }
                   }),
                   Posts= x.Posts.Select(x => new GetPostDTO
                   {

                       Id = x.Id,
                       CreatedAt = x.CreatedAt,
                       Heading = x.Heading,
                       Image = x.Image,
                       ReadTime = x.ReadTime,
                       Text = x.Text


                   })
            }).ToList()
            };
            return response;
        }
    }
}
