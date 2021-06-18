using Blog.Application;
using Blog.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Logging
{
    public class UseCaseLogger : IUseCaseLogger
    {
        private readonly BlogContext _context;

        public UseCaseLogger(BlogContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object data)
        {
            _context.Logs.Add(new Domain.Log
            {
                Actor = actor.Identity,
                Data = JsonConvert.SerializeObject(data),
                UseCaseName = useCase.Name
            });

            _context.SaveChanges();
        }
    }
}
