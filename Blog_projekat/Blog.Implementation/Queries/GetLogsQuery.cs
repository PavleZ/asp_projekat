using AutoMapper;
using Blog.Application.DTO.LogDTOs;
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
    public class GetLogsQuery : IGetLogsQuery
    {
        private readonly BlogContext _context;

        public GetLogsQuery(BlogContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Log search.";

        public PagedResponse<GetLogsDTO> Execute(LogSearch search)
        {
            var query = _context.Logs.AsQueryable();
            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);

            }

            if (!string.IsNullOrEmpty(search.UseCase) || !string.IsNullOrWhiteSpace(search.UseCase))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCase));
            }


            if (!string.IsNullOrEmpty(search.Username) || !string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.Actor.ToLower().Contains(search.Username));
            }


            if ((search.StartDate.HasValue && search.EndDate.HasValue) && (search.StartDate < search.EndDate))
            {
                query = query.Where(x => x.CreatedAt >= search.StartDate && x.CreatedAt <= search.EndDate);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetLogsDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetLogsDTO
                {
                    Id=x.Id,
                    UseCase=x.UseCaseName,
                    User=_context.Users.Where(u=> u.Username == x.Actor).Select(y=> new UserDTO { 
                        Id=y.Id,
                        Username=y.Username,
                        Email=y.Email,
                        FirstName=y.FirstName,
                        LastName=y.LastName
                    
                    }).FirstOrDefault(),
                    CreatedAt=x.CreatedAt,
                    Data=x.Data
                }).ToList()
            };
            return response;
        }
    }
}
