using Blog.Application.DTO.LogDTOs;
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
    public class GetLogQuery : IGetLogQuery
    {
        private readonly BlogContext _context;

        public GetLogQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 21;

        public string Name => "Get one log";

        public GetLogsDTO Execute(int search)
        {
            var query = _context.Logs.Find(search);

            if (query == null)
                throw new EntityNotFoundException(search, typeof(Log));

            return new GetLogsDTO
            {
                Id = query.Id,
                CreatedAt = query.CreatedAt,
                Data = query.Data,
                UseCase = query.UseCaseName,
                User = _context.Users.Where(x => x.Username == query.Actor).Select(y => new UserDTO
                {
                    Id = y.Id,
                    Email = y.Email,
                    FirstName = y.FirstName,
                    Username = y.Username,
                    LastName = y.LastName

                }).FirstOrDefault()

            };
        }
    }
}
