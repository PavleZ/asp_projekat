using Blog.Application.Commands.CategoryCommands;
using Blog.Application.DTO.CategoryDTOs;
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
    public class CreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly BlogContext _context;
        private readonly CreateCategoryValidator _validator;

        public CreateCategoryCommand(BlogContext context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Create new Category";




        public void Execute(CategoryDTO request)
        {
            _validator.ValidateAndThrow(request);

            var category = new Category
            {
                Name = request.Name

            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            
        }
    }
}
