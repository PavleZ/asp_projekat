using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class PostRating : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }




    }
}
