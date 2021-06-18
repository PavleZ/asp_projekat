using Blog.Application.DTO.SentMailDTOs;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
   public interface IGetSentMailsQuery : IQuery<SentMailSearch, PagedResponse<SentMailDTO>>
    {
    }
}
