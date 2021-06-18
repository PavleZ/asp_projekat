using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
   public class User : EntityWithId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }



        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<UserUseCase> UserUseCases { get; set; } = new HashSet<UserUseCase>();
        public ICollection<PostRating> PostRatings { get; set; } = new HashSet<PostRating>();


        



    }
}
