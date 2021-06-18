using Blog.Application.DTO.PostDTOs;
using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.PostRatingDTOs
{
    public class GetPostRatingDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }

        public GetPostDTO Post { get; set; }
        public UserDTO User { get; set; }

    }
}
