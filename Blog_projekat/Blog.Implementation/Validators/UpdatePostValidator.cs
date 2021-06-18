using Blog.Application.DTO.PostDTOs;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class UpdatePostValidator : AbstractValidator<UpdatePostDTO>
    {
        public UpdatePostValidator(BlogContext context)
        {
            RuleFor(x => x.Heading).NotEmpty().WithMessage("Heading is required")
                .Must((dto, heading) => !context.Posts.Any(x => x.Heading == heading && x.Id != dto.Id))
                    .WithMessage("Post with given heading already exists!");

            RuleFor(x => x.Text).NotEmpty().WithMessage("Post text is required")
                .Must((dto, text) => !context.Posts.Any(x => x.Text == text && x.Id != dto.Id))
                    .WithMessage("Post with given text  already exists!");

            RuleFor(x => x.Image).NotNull().WithMessage("Post image is required!");
            RuleFor(x => x.ReadTime).NotNull().GreaterThan(0).WithMessage("Read time is required and must be above 0!");
            RuleFor(x => x.Categories).Must(x => x.ToString().Split(",").ToList().Count() > 0).WithMessage("Post need to have at least one category ").DependentRules(() => {

                RuleFor(x => x.Categories).Must(x => x.ToString().TrimStart('[').TrimEnd(']').Split(",").ToList().Count() == x.ToString().TrimStart('[').TrimEnd(']').Split(",").Distinct().Count())
                    .WithMessage("Post can't belong to same category twice, categories must be different to one another");

            });
        }
    }
}
