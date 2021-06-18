using Blog.Application.Commands.PostCommands;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Commands.PostCommands
{
    public class UpdatePostCommand : IUpdatePostCommand
    {
        private readonly BlogContext _context;
        private readonly UpdatePostValidator _validator;

        public UpdatePostCommand(BlogContext context, UpdatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Update post.";

        public void Execute(UpdatePostDTO request)
        {
            _validator.ValidateAndThrow(request);

            var post = _context.Posts.Include(x => x.PostCategories).FirstOrDefault(x => x.Id == request.Id);

            if(post == null)
                throw new EntityNotFoundException(request.Id, typeof(SentEmails));


            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(request.Image.FileName);

            var newFileName = guid + extension;

            var path = Path.Combine("wwwroot", "images", newFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                request.Image.CopyTo(fileStream);
            }

            post.Heading = request.Heading;
            post.Text = request.Text;
            post.ReadTime = Convert.ToInt32(request.ReadTime);
            post.Image = newFileName;

            var currentCurrentCategories = _context.PostCategories.Where(x => x.PostId == request.Id);
            foreach (var item in currentCurrentCategories)
            {
                item.IsDeleted = true;
                item.DeletedAt = DateTime.Now;
                item.IsActive = false; 
                
                    
            }
            
           
            _context.SaveChanges();
            




            var categories = request.Categories.First().Split(',').ToList();


            foreach (var cat in categories)
            {
                _context.PostCategories.Add(new PostCategory
                {

                    Post = post,
                    CategoryId =Convert.ToInt32(cat)
                });
            }
            _context.SaveChanges();





        }
    }
}
