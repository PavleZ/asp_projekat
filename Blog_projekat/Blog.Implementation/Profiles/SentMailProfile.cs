using AutoMapper;
using Blog.Application.DTO.SentMailDTOs;
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
   public class SentMailProfile : Profile
    {
        public SentMailProfile(BlogContext context)
        {
            CreateMap<SentEmails, SentMailDTO>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.To, opt => opt.MapFrom(s => s.To))
                    .ForMember(d => d.From, opt => opt.MapFrom(s => s.From))
                    .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
                    .ForMember(d => d.Subject, opt => opt.MapFrom(s => context.Users.Where(x => x.Email == s.To).Select(x => new UserDTO
                    {

                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Username = x.Username

                    })));






        }
    }
}
