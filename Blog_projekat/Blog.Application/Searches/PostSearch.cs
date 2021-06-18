using Blog.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Searches
{
    public class PostSearch : PagedSearch
    {
        public int? Id { get; set; }

        public string Keyword { get; set; } 
        public int? Rating { get; set; }
        public int? ReadTime { get; set; }




    }
}
