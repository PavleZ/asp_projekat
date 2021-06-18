using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }


        public UserDTO User { get; set; }
        public GetPostDTO Post { get; set; }

    }
}
