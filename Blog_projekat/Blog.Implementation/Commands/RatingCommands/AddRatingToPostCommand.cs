using Blog.Application;
using Blog.Application.Commands.RatingCommands;
using Blog.Application.DTO;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.RatingCommands
{
    public class AddRatingToPostCommand : IAddRatingToPostCommand
    {
        private readonly BlogContext _context;
        private readonly  AddRatingToPostValidator _validator;
        private readonly IApplicationActor _actor;


        public AddRatingToPostCommand(BlogContext context, AddRatingToPostValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 11;

        public string Name => "Rate post.";

        public void Execute(AddRatingToPostDTO request)
        {
            _validator.ValidateAndThrow(request);

            var postRating = new PostRating
            {
                PostId = request.PostId,
                UserId = _actor.Id,
                Rating = request.Rating
            };

            _context.PostRatings.Add(postRating);
            _context.SaveChanges();
        }
    }
}
