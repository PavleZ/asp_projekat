using Blog.Application;
using Blog.Application.Commands.CommentCommands;
using Blog.Application.DTO.CommentDTOs;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.CommentCommands
{
    public class UpdateCommentCommand : IUpdateCommentCommand
    {
        private readonly BlogContext _context;
        private readonly UpdateCommentValidator _validator;
        private readonly IApplicationActor _actor;


        public UpdateCommentCommand(BlogContext context, UpdateCommentValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 9;

        public string Name => "Comment update.";

        public void Execute(UpdateCommentDTO request)
        {
            _validator.ValidateAndThrow(request);

            var comment = _context.Comments.Include(x=>x.User).FirstOrDefault(x=> x.Id == request.Id);
            if (comment == null)
            throw new EntityNotFoundException(request.Id, typeof(Comment));

            if (_actor.Id != comment.User.Id)
                throw new UnauthorizedUseCaseException(this,_actor);

            comment.Heading = request.Heading;
            comment.Body = request.Body;
            _context.SaveChanges();


        }
    }
}
