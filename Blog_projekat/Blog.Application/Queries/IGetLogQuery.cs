using Blog.Application.DTO.LogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
    public interface IGetLogQuery : IQuery<int, GetLogsDTO>
    {

    }
}
