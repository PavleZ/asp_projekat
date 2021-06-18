using Blog.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Searches
{
    public class CategorySearch:PagedSearch
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
