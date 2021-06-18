using Blog.Application;
using Blog.Application.Commands.CommentCommands;
using Blog.Application.DTO.CommentDTOs;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.CommentCommands
{
    public class CreateCommentCommand : ICreateCommentCommand
    {
        private readonly BlogContext _context;
        private readonly CreateCommentValidator _validator;
        private readonly IApplicationActor _actor;



        public CreateCommentCommand(BlogContext context, CreateCommentValidator validator,IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 8;

        public string Name => "Create comment.";

        public void Execute(CreateCommentDTO request)
        {
            _validator.ValidateAndThrow(request);

            var comment = new Comment
            {
                Heading=request.Heading,
                Body=request.Body,
                ParentId=request.ParentId,
                PostId=request.PostId,
                UserId=_actor.Id
                
            };
            var postComment = new PostComment
            {
                PostId = request.PostId,
                Comment = comment
            };


            _context.PostComments.Add(postComment);
            _context.SaveChanges();

        }
    }
}
