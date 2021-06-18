using AutoMapper;
using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.DTO.CommentDTOs;
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
    public class GetPostsQuery : IGetPostsQuery
    {
        private readonly BlogContext _context;

        public GetPostsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 14;

        public string Name => "Post search";

        public PagedResponse<PostDTO> Execute(PostSearch search)
        {
            var query = _context.Posts.Include(x => x.PostCategories).ThenInclude(x => x.Category)
                .Include(x => x.PostComments).ThenInclude(x => x.Comment).Include(x => x.PostRatings).AsQueryable();

            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.Heading.ToLower().Contains(search.Keyword.ToLower()) ||
                x.Text.ToLower().Contains(search.Keyword.ToLower()) || x.User.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                x.User.LastName.ToLower().Contains(search.Keyword.ToLower()) ||
                x.PostComments.Any(y => y.Comment.Heading.ToLower().Contains(search.Keyword.ToLower()) || y.Comment.Body.ToLower().Contains(search.Keyword.ToLower()))
                || x.PostCategories.Any(y => y.Category.Name.ToLower().Contains(search.Keyword.ToLower())));
            }
            if (search.Rating.HasValue)
            {
                query = query.Where(x => x.PostRatings.Any(y => y.Rating >= search.Rating));
            }
            if (search.ReadTime.HasValue)
            {
                query = query.Where(x => x.ReadTime >= search.ReadTime);

            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<PostDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new PostDTO
                {
                    Id = x.Id,
                    Image = x.Image,
                    AverageRating = (float)x.PostRatings.Select(x => x.Rating).Average(),
                    Heading = x.Heading,
                    CreatedAt = x.CreatedAt,
                    ReadTime = x.ReadTime,
                    Text = x.Text,
                    Ratings = x.PostRatings.Select(x => x.Rating),
                    Categories = x.PostCategories.Select(y => new CategoryDTO
                    {
                        Id = y.Category.Id,
                        Name = y.Category.Name

                    }),
                    Comments = x.PostComments.Select(y => new CommentDTO
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
                    })


                }).ToList()
            
            };

            return response;
        }
    }
}
