using Blog.Application.DTO.CategoryDTOs;
using Blog.Application.DTO.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.PostDTOs
{
    public class PostDTO 
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public int ReadTime { get; set; }
        public float? AverageRating { get; set; }

        public DateTime CreatedAt { get; set; }


        public IEnumerable<CategoryDTO>? Categories { get; set; } = new List<CategoryDTO>();

        public IEnumerable<CommentDTO>? Comments { get; set; } = new List<CommentDTO>();
        public IEnumerable<int>? Ratings { get; set; } = new List<int>();






    }
}
