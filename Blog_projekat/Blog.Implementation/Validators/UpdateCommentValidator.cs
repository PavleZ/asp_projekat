using Blog.Application.DTO.CommentDTOs;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class UpdateCommentValidator:AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator(BlogContext context)
        {
            RuleFor(x => x.Heading).NotEmpty().WithMessage("Comment heading is required.").DependentRules(() =>
            {
                RuleFor(x => x.Heading).Must((dto, heading) => !context.Comments.Any(x => x.Heading == heading && x.Id != dto.Id))
                    .WithMessage(x => $"Comment with heading: {x.Heading} already exists in database!");

            });

            RuleFor(x => x.Body).NotEmpty().WithMessage("Comment body is required.").DependentRules(() =>
            {
                RuleFor(x => x.Body).Must((dto, body) => !context.Comments.Any(x => x.Body == body && x.Id != dto.Id))
                    .WithMessage(x => $"Comment with body : {x.Body} already exists in database!");

            });
        }
    }
}
