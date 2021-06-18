using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Helper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDatabaseController : ControllerBase
    {
        private readonly BlogContext _context;
        public SeedDatabaseController(BlogContext context)
        {
            _context = context;
        }

        // POST api/<SeedDatabaseController>
        [HttpPost]
        public IActionResult Post()
        {
            var adminUser = new User
            {
                FirstName = "admin",
                LastName = "admin",
                Password = Utilities_Helper.MD5("admin123"),
                Email = "admin@gmail.com",
                Username = "admin"

            };
            var adminUseCases = new List<int> {
             1,3,4,10,7,12,13,8,9,5,6,11,21,16,23,17,24,18,19,20,15,22,14
            };
            _context.Users.Add(adminUser);
            var userUseCase = new List<UserUseCase>();
            foreach (var useCase in adminUseCases)
            {
                userUseCase.Add(new UserUseCase
                {
                    User = adminUser,
                    UseCaseId = useCase
                });
            }
            _context.UserUseCases.AddRange(userUseCase);



            var usersFaker = new Faker<User>();

            usersFaker.RuleFor(x => x.FirstName, f => f.Name.FirstName());
            usersFaker.RuleFor(x => x.LastName, f => f.Name.LastName());
            usersFaker.RuleFor(x => x.Email, f => f.Person.Email);
            usersFaker.RuleFor(x => x.Username, f => f.Internet.UserName());
            usersFaker.RuleFor(x => x.Password, Utilities_Helper.MD5("sifra123"));


            var users = usersFaker.Generate(10);
            _context.Users.AddRange(users);
            _context.SaveChanges();

            var userIds = _context.Users.Where(x=>x.Username != "admin").Select(x => x.Id).OrderBy(x=> x).ToList();
            var commonUseCases = new List<int> { 8, 9, 5, 6, 11, 22, 20, 15, 19, 14 };
            var userUseCases = new List<UserUseCase>();

            for(int i=0; i < userIds.Count; i++)
            {
                foreach (var useCase in commonUseCases)
                {
                    userUseCases.Add(new UserUseCase
                    {
                        UserId = userIds[i],
                        UseCaseId = useCase
                    });
                    
                }
            }
            _context.UserUseCases.AddRange(userUseCases);
            _context.SaveChanges();






            var categoriesFaker = new Faker<Category>();
            categoriesFaker.RuleFor(x => x.Name, f => f.Lorem.Random.Word());

            var categories = categoriesFaker.Generate(15);
            _context.Categories.AddRange(categories);
            _context.SaveChanges();


            var categoryIds = _context.Categories.Select(x => x.Id).OrderBy(x => x).ToList();



            var postsFaker = new Faker<Post>();
            postsFaker.RuleFor(x => x.Heading, f => f.Lorem.Sentence());
            postsFaker.RuleFor(x => x.Text, f => f.Lorem.Paragraph());
            postsFaker.RuleFor(x => x.Image, f => f.Image.LoremFlickrUrl());
            postsFaker.RuleFor(x => x.ReadTime, f => f.PickRandom(new List<int> {2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20}));
            postsFaker.RuleFor(x => x.UserId, f => f.PickRandom(userIds));


            var posts = postsFaker.Generate(20);

            _context.Posts.AddRange(posts);
            _context.SaveChanges();

            var commentsFaker = new Faker<Comment>();

            var postIds = _context.Posts.Select(x => x.Id).OrderBy(x => x).ToList();


            commentsFaker.RuleFor(x => x.Heading, f => f.Lorem.Sentence());
            commentsFaker.RuleFor(x => x.Body, f => f.Lorem.Text());
            commentsFaker.RuleFor(x => x.PostId, f => f.PickRandom(postIds));
            commentsFaker.RuleFor(x => x.UserId, f => f.PickRandom(userIds));

            var comments = commentsFaker.Generate(20);
            _context.Comments.AddRange(comments);
            _context.SaveChanges();


            var commentIds = _context.Comments.Select(x => x.Id).OrderBy(x => x).ToList();



            // postCategories, postComments,postRatings

            var postCategoriesFaker = new Faker<PostCategory>();

            postCategoriesFaker.RuleFor(x => x.CategoryId, f => f.PickRandom(categoryIds));
            postCategoriesFaker.RuleFor(x => x.PostId, f => f.PickRandom(postIds));

            var postCategories = postCategoriesFaker.Generate(10);


            var postRatingsFaker = new Faker<PostRating>();

          
            
            postRatingsFaker.RuleFor(x => x.PostId, f=>f.PickRandom(postIds));
            postRatingsFaker.RuleFor(x => x.UserId, f => f.PickRandom(userIds));
            postRatingsFaker.RuleFor(x => x.Rating, f => f.PickRandom(new List<int> { 1,2, 3, 4, 5}));

            var postRatings = postRatingsFaker.Generate(10);

            var postcommentFaker = new Faker<PostComment>();

            postcommentFaker.RuleFor(x => x.PostId, f => f.PickRandom(postIds));
            postcommentFaker.RuleFor(x => x.CommentId, f => f.PickRandom(commentIds));

            var postComments = postcommentFaker.Generate(5);


            _context.PostRatings.AddRange(postRatings);
            _context.PostCategories.AddRange(postCategories);
            _context.PostComments.AddRange(postComments);


            _context.SaveChanges();

            return Ok();



        }



    }
}
