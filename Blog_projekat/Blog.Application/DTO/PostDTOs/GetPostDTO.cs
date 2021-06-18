﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.PostDTOs
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int ReadTime { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
