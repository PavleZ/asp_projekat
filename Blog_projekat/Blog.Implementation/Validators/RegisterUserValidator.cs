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
   public  class RegisterUserValidator: AbstractValidator<UserRegisterDTO>
    {
        public RegisterUserValidator(BlogContext context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.").DependentRules(()=> {

                RuleFor(x => x.Username).Must(username => !context.Users.Any(x => x.Username == username))
                                .WithMessage("Username must be unique, given already exists!");
            });
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").DependentRules(() => {

                RuleFor(x => x.Email).Must(email => !context.Users.Any(x => x.Email == email))
                                .WithMessage("Email must be unique, given already exists!");
            });

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");



        }
    }
}
