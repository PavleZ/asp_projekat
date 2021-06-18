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
   public class CreatePostValidator : AbstractValidator<CreatePostDTO>
    {
        public CreatePostValidator(BlogContext context)
        {
            
            RuleFor(x => x.Heading).NotEmpty().WithMessage("Post heading is required!").DependentRules(() => {

                RuleFor(x => x.Heading).Must(x => !context.Posts.Any(y => y.Heading == x)).WithMessage("Post heading need to be unique!");
            });
            RuleFor(x => x.Text).NotEmpty().WithMessage("Post text is required!").DependentRules(()=>
            {
                RuleFor(x => x.Text).Must(x => !context.Posts.Any(y => y.Text == x)).WithMessage("Post text need to be unique!");

            });
            RuleFor(x => x.Image).NotNull().WithMessage("Post image is required!");
            RuleFor(x => x.ReadTime).NotNull().GreaterThan(0).WithMessage("Read time is required and must be above 0!");
            RuleFor(x => x.Categories).Must(x => x.ToString().Split(",").ToList().Count() > 0).WithMessage("Post need to have at least one category ").DependentRules(() =>{

                RuleFor(x => x.Categories).Must(x => x.ToString().TrimStart('[').TrimEnd(']').Split(",").ToList().Count() == x.ToString().TrimStart('[').TrimEnd(']').Split(",").Distinct().Count())
                    .WithMessage("Post can't belong to same category twice, categories must be different to one another");

            });









        }

    }
}
