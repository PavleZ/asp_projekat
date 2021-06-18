using Blog.Application.Commands.CommentCommands;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.CommentCommands
{
    public class DeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly BlogContext _context;

        public DeleteCommentCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 10;

        public string Name => "Delete comment";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);

            if (comment == null)
            {
                throw new EntityNotFoundException(request, typeof(Comment));

            }

            comment.DeletedAt = DateTime.Now;
            comment.IsActive = false;
            comment.IsDeleted = true;
            comment.ModifiedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
