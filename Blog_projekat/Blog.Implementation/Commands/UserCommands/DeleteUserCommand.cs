using Blog.Application.Commands.UserCommands;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.UserCommands
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly BlogContext _context;

        public DeleteUserCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 13;

        public string Name => "Delete user";

        public void Execute(int request)
        {
            var user = _context.Categories.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));

            }

            user.DeletedAt = DateTime.Now;
            user.IsActive = false;
            user.IsDeleted = true;
            user.ModifiedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
