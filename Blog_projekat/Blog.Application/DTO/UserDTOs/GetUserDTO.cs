﻿using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.PostRatingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.UserDTOs
{
    public class GetUserDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public IEnumerable<GetPostDTO> Posts { get; set; }
        public IEnumerable<GetCommentDTO> Comments { get; set; }
        public IEnumerable<GetPostRatingDTO> Ratings { get; set; }





    }
}
