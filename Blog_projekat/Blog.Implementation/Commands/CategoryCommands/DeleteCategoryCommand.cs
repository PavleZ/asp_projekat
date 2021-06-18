using Blog.Application.Commands.CategoryCommands;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly BlogContext _context;

        public DeleteCategoryCommand(BlogContext context)
        {
            _context = context;
        }

        public int Id => 4;

        public string Name => "Delete category.";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);

            if(category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));

            }

            category.DeletedAt = DateTime.Now;
            category.IsActive = false;
            category.IsDeleted = true;
            category.ModifiedAt = DateTime.Now;

            _context.SaveChanges();



        }
    }
}
