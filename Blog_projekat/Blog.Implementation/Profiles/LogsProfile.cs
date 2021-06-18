using AutoMapper;
using Blog.Application.DTO.LogDTOs;
using Blog.Application.DTO.UserDTOs;
using Blog.DataAccess;
using Blog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Profiles
{
    public class LogsProfile : Profile
    {

        public LogsProfile(BlogContext context)
        {
            CreateMap<Log, GetLogsDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(c => c.CreatedAt))
                .ForMember(d => d.UseCase, opt => opt.MapFrom(c => c.UseCaseName))
                .ForMember(d => d.Data, opt => opt.MapFrom(c => c.Data))
                .ForMember(d => d.User, opt => opt.MapFrom(c => context.Users.Where(x => x.Username == c.Actor).Select(y => new UserDTO
                {

                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Username = y.LastName,
                    Email = y.Email

                })));





        }
    }
}
