using Blog.Application.Commands.PostCommands;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.PostCommands
{
    public class DeletePostCommand : IDeletePostCommand
    {
        private readonly BlogContext _context;

        public DeletePostCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 7;

        public string Name => "Delete Post";

        public void Execute(int request)
        {
            var post = _context.Posts.Find(request);

            if (post == null)
            {
                throw new EntityNotFoundException(request, typeof(SentEmails));

            }

            post.DeletedAt = DateTime.Now;
            post.IsActive = false;
            post.IsDeleted = true;
            post.ModifiedAt = DateTime.Now;

            _context.SaveChanges();
        }




       
  
    }
}
