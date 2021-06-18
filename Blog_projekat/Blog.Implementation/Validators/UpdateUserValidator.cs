using Blog.Application.DTO.UserDTOs;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator(BlogContext context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").DependentRules(()=> {
                RuleFor(x => x.Email).Must((dto, email) => !context.Users.Any(y => y.Email == email && y.Id != dto.Id))
                .WithMessage("Email needs to be uniqe, given already exists in database!");
            });


            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required").DependentRules(() => {
                RuleFor(x => x.Username).Must((dto, username) => !context.Users.Any(y => y.Username == username && y.Id != dto.Id))
                .WithMessage("Username needs to be uniqe, given already exists in database!");
            });


        }
    }
}
