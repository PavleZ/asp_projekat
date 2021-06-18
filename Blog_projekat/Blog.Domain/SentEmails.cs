using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public class SentEmails:EntityWithId
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }



    }
}
