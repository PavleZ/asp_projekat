using Blog.Application;
using Blog.Application.DTO;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class AddRatingToPostValidator : AbstractValidator<AddRatingToPostDTO>
    {
        public AddRatingToPostValidator(BlogContext context, IApplicationActor actor)
        {
            RuleFor(x => x.PostId).Must(x => context.Posts.Any(y => y.Id == x)).WithMessage("Post with given id doesn't exist.")
                   .DependentRules(()=> {
                       RuleFor(x => x.PostId).Must(x => !context.PostRatings.Any(y => y.PostId == x && y.UserId == actor.Id))
                      .WithMessage("Post with given ID has already been rated.");
                   
                   });
            RuleFor(x => x.Rating).GreaterThan(0).LessThanOrEqualTo(5);

        }



    }
}
