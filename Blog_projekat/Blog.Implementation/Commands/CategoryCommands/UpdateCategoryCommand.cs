using Blog.Application.Commands.CategoryCommands;
using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly BlogContext _context;
        private readonly UpdateCategoryValidator _validator;

        public UpdateCategoryCommand(BlogContext context, UpdateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Update Category.";

        public void Execute(CategoryDTO request)
        {
            _validator.ValidateAndThrow(request);

            var category = _context.Categories.Find(request.Id);

            if(category == null)
            {
                throw new EntityNotFoundException(Convert.ToInt32(request.Id), typeof(Category));

            }

            category.Name = request.Name;
            _context.SaveChanges();

        }
    }
}
