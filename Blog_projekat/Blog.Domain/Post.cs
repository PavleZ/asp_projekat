using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Post : EntityWithId
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
        public int ReadTime { get; set; }

        public User User { get; set; }
        public ICollection<PostComment> PostComments { get; set; } = new HashSet<PostComment>();

        public ICollection<PostCategory> PostCategories { get; set; } = new HashSet<PostCategory>();
        public ICollection<PostRating> PostRatings { get; set; } = new HashSet<PostRating>();











    }
}
