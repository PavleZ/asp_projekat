using Blog.Application.DTO.CommentDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
   public class CreateCommentValidator:AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Heading).NotEmpty().WithMessage("Comment heading is required!");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Comment body is required!");
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("Id of post is required!");

        }
    }
}
