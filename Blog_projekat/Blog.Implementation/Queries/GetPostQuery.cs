using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.DTO.CommentDTOs;
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
    public class GetPostQuery : IGetPostQuery
    {
        private readonly BlogContext _context;

        public GetPostQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 22;

        public string Name => "Get one post";

        public PostDTO Execute(int search)
        {

            var query = _context.Posts.Include(x => x.PostCategories).ThenInclude(x => x.Category)
                .Include(x => x.PostComments).ThenInclude(x => x.Comment).ThenInclude(x=>x.User).Include(x => x.PostRatings).FirstOrDefault(x=>x.Id == search);

            if (query == null)
                throw new EntityNotFoundException(search, typeof(Post));

            return new PostDTO
            {

                Id = query.Id,
                Image = query.Image,
                AverageRating = (float) calculateAverage(query.PostRatings.Select(x => x.Rating)),
                Heading = query.Heading,
                CreatedAt = query.CreatedAt,
                ReadTime = query.ReadTime,
                Text = query.Text,
                Ratings = query.PostRatings.Select(x => x.Rating),
                Categories = query.PostCategories.Select(x => new CategoryDTO
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name

                }),
                Comments = query.PostComments.Select(y => new CommentDTO
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
                        Username = y.Comment.User.Username

                    } 
                

                })

               
            };



        }
        public float? calculateAverage(IEnumerable<int> collection)
        {
            if (!collection.Any())
                return 0;
            return  (float )collection.Average();
        }
    }

   
}
