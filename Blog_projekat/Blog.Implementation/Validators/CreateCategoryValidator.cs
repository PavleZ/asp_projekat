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
    public class CreateCategoryValidator : AbstractValidator<CategoryDTO>
    {

        public CreateCategoryValidator(BlogContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required!")
                .DependentRules(() =>
                {

                    RuleFor(x => x.Name).Must(name => !context.Categories.Any(x => x.Name == name))
                                        .WithMessage("Category must be unique, given already exists!");

                });

        }
    }
}
