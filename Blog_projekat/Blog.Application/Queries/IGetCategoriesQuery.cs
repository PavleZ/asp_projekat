using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
    public interface IGetCategoriesQuery : IQuery<CategorySearch,PagedResponse<CategoryDTO>>
    {
    }
}
