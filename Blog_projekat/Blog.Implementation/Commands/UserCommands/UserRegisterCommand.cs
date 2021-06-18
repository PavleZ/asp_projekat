using Blog.Application.Commands.UserCommands;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Email;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Helper;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.UserCommands
{
    public class UserRegisterCommand : IRegisterUserCommand
    {
        private readonly BlogContext _context;
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _sender;

        public UserRegisterCommand(BlogContext context, RegisterUserValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 2;

        public string Name => "User Registration.";

        public void Execute(UserRegisterDTO request)
        {
            _validator.ValidateAndThrow(request);
            var commonUseCases = new List<int>{8,9,5,6,11,22,20,15,19,14};
            var user = new User
            {

                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                Password = Utilities_Helper.MD5(request.Password)

            };
            _context.Users.Add(user);

            var userUseCase = new List<UserUseCase>();
            foreach (var useCase in commonUseCases)
            {
                userUseCase.Add(new UserUseCase
                {
                    User = user,
                    UseCaseId = useCase
                });
            }
            _context.UserUseCases.AddRange(userUseCase);



            var senderObj = new SendEmailDto
            {
                Content = "<h1>Uspesno ste se registrovali!</h1>",
                SendTo = request.Email,
                Subject = "Blog-asp_projekat-registracija"
            };
            _sender.Send(senderObj);
            var email = new SentEmails
            {
                Content = senderObj.Content,
                Subject = senderObj.Subject,
                To = senderObj.SendTo,
                From = _sender.FromEmail

            };
            _context.SentEmails.Add(email);


            _context.SaveChanges();

        }

    }
}
