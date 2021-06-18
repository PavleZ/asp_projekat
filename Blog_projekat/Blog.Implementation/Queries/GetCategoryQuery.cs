using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Queries
{
    public class GetCategoryQuery : IGetCategoryQuery
    {
        private readonly BlogContext _context;

        public GetCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 19;

        public string Name => "Get one category.";

        public CategoryDTO Execute(int search)
        {
            var query = _context.Categories.Find(search);

            if(query == null)
                throw new EntityNotFoundException(search, typeof(Category));
            return new CategoryDTO
            {
                Id = query.Id,
                Name = query.Name
            };



        }
    }
}
