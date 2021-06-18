using Blog.Application.DTO.PostDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
    public interface IGetPostQuery : IQuery<int, PostDTO>
    {
    }
}
