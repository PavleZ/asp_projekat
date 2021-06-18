using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.SentMailDTOs
{
    public class SentMailDTO
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; }


        public UserDTO User { get; set; }
    }
}
