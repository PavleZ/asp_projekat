using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
   public  class AddRatingToPostDTO
    {
        public int PostId { get; set; }
        public int Rating { get; set; }

    }
}
