using Blog.Application.DTO.SentMailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
   public  interface IGetSentMailQuery : IQuery<int, SentMailDTO>
    {
    }
}
