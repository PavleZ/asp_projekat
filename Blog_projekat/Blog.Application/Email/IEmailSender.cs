using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Email
{
    public interface IEmailSender
    {
        void Send(SendEmailDto dto);
        public string FromEmail { get;  }
    }

    public class SendEmailDto
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SendTo { get; set; }
    }
}
