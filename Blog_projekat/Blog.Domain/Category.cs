using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Category : EntityWithId
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<PostCategory> PostCategories = new HashSet<PostCategory>();


    }
}
