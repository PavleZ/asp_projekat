using Blog.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Searches
{
    public class SentMailSearch :  PagedSearch
    {
        public int? Id { get; set; }

        public string To { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


    }
}
