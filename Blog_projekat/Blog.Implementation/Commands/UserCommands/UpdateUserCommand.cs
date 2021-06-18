using Blog.Application.Commands.UserCommands;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.UserCommands
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly BlogContext _context;
        private readonly UpdateUserValidator _validator;

        public UpdateUserCommand(BlogContext context, UpdateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "User update.";

        public void Execute(UpdateUserDTO request)
        {
            _validator.ValidateAndThrow(request);

            var user = _context.Users.Find(request.Id);

            if(user == null)
                throw new EntityNotFoundException(request.Id, typeof(User));

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            if(request.Password !=null)
                user.Password = request.Password;
            user.Email = request.Email;
            user.Username = request.Username;

            _context.SaveChanges();




        }
    }
}
