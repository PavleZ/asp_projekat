using Blog.Application.DTO.CategoryDTOs;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class UpdateCategoryValidator :  AbstractValidator<CategoryDTO>
    {
        public UpdateCategoryValidator(BlogContext _context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must((dto, name) => !_context.Categories.Any(x => x.Name == name && x.Id != dto.Id))
                    .WithMessage(x => $"Category with name: {x.Name} already exists in database!");

            });
        }
        
    }
}
