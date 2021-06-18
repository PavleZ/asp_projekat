using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Comment : EntityWithId
    {
        public string Heading { get; set; }
        public string Body { get; set; }
        public int PostId{ get; set; }
        public int? ParentId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();
        public Comment ParentComment { get; set; } 










    }
}
