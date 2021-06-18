using AutoMapper;
using Blog.Application.DTO.CategoryDTOs;
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
    public class GetCategoriesQuery : IGetCategoriesQuery
    {
        private readonly BlogContext _context;
        public GetCategoriesQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "Category search.";

        public PagedResponse<CategoryDTO> Execute(CategorySearch search)
        {
            var query = _context.Categories.AsQueryable();

            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }
            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<CategoryDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            return response;
        }
    }
}
