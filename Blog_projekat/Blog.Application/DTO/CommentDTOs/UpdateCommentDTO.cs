using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.CommentDTOs
{
    public class UpdateCommentDTO
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
    }
}
