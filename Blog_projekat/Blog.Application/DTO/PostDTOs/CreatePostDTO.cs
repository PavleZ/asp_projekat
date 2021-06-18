using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.PostDTOs
{
    public class CreatePostDTO
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
        public int? ReadTime { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
