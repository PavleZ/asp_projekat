using Blog.Application.DTO.PostDTOs;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
    public interface IGetPostsQuery : IQuery<PostSearch, PagedResponse<PostDTO>>
    {
    }
}
