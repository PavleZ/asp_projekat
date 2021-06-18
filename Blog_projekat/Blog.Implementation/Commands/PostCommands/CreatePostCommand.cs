using Blog.Application;
using Blog.Application.Commands.PostCommands;
using Blog.Application.DTO.PostDTOs;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.PostCommands
{
    public class CreatePostCommand : ICreatePostCommand
    {
        private readonly BlogContext _context;
        private readonly CreatePostValidator _validator;
        private readonly IApplicationActor _actor;


        public CreatePostCommand(BlogContext context, CreatePostValidator validator, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _actor = actor;
        }

        public int Id => 5;

        public string Name => "Create new post.";

        public void Execute(CreatePostDTO request)
        {
            _validator.ValidateAndThrow(request);

            //---------------------------------------------------------------------------
            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(request.Image.FileName);

            var newFileName = guid + extension;

            var path = Path.Combine("wwwroot", "images", newFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                request.Image.CopyTo(fileStream);
            }
            //---------------------------------------------------------------------------

            var post = new Post
            {
                Heading=request.Heading,
                Text=request.Text,
                Image= newFileName,
                UserId=_actor.Id,
                ReadTime=Convert.ToInt32(request.ReadTime)
            };
            // "101,102,103"
            var categories = request.Categories.First().Split(',').ToList();
            foreach(var cat in categories)
            {
                
                _context.PostCategories.Add(new PostCategory
                {

                    Post = post,
                    CategoryId=Convert.ToInt32(cat)
                });
            }

            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
