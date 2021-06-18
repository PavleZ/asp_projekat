using Blog.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Core
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => -1;

        public string Identity => "Unauthorized";

        public IEnumerable<int> AllowedUseCases => new List<int>{2,11,22,20,15,19,14};
    }
}
