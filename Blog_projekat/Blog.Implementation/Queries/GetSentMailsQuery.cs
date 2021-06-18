using AutoMapper;
using Blog.Application.DTO.SentMailDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.Application.Queries;
using Blog.Application.Searches;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Queries
{
    public class GetSentMailsQuery : IGetSentMailsQuery
    {
        private readonly BlogContext _context;

        public GetSentMailsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 17;

        public string Name => "Search sent emails.";

        public PagedResponse<SentMailDTO> Execute(SentMailSearch search)
        {

            var query = _context.SentEmails.AsQueryable();

            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.To) || !string.IsNullOrWhiteSpace(search.To))
            {
                query = query.Where(x => x.To.ToLower().Contains(search.To));
            }

          


            if ((search.StartDate.HasValue && search.EndDate.HasValue) && (search.StartDate < search.EndDate))
            {
                query = query.Where(x => x.CreatedAt >= search.StartDate && x.CreatedAt <= search.EndDate);
            }


            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<SentMailDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new SentMailDTO
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    Content = x.Content,
                    From = x.From,
                    Subject = x.Subject,
                    To = x.To,
                    User = _context.Users.Where(y => y.Email == x.To).Select(c => new UserDTO {
                        Id = c.Id,
                        Email = c.Email,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Username = c.Username

                    }).First()
                   
                }).ToList()
            };
            return response;
        }
    }
}
