using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.CommentDTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }


        public UserDTO User { get; set; }


    }
}
