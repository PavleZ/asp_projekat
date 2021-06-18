using Blog.Application.DTO.UserDTOs;
using Blog.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries
{
   public  interface IGetUsersQuery : IQuery<UserSearch, PagedResponse<GetUserDTO>>
    {


    }
}
