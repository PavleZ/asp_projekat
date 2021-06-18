using Blog.Application.DTO.SentMailDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Exceptions;
using Blog.Application.Queries;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Queries
{
    public class GetSentMailQuery : IGetSentMailQuery
    {
        private readonly BlogContext _context;

        public GetSentMailQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 23;

        public string Name => "Get one sent mail";

        public SentMailDTO Execute(int search)
        {
            var query = _context.SentEmails.Find(search);

            if (query == null)
                throw new EntityNotFoundException(search, typeof(SentEmails));


            return new SentMailDTO
            {
                Id = query.Id,
                CreatedAt = query.CreatedAt,
                Content = query.Content,
                From = query.From,
                To = query.To,
                Subject = query.Subject,
                User = _context.Users.Where(x => x.Email == query.To).Select(y => new UserDTO
                {

                    Id = y.Id,
                    Email = y.Email,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Username = y.Username
                }).First()


            };
        }
    }
}
