using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class Log : EntityWithId
    {

        public string Data { get; set; }
        public string Actor { get; set; }
        public string UseCaseName { get; set; }



    }
}
