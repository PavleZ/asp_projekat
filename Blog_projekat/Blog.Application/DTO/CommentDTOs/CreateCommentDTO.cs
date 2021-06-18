using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Heading { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public int? ParentId { get; set; }

    }
}
