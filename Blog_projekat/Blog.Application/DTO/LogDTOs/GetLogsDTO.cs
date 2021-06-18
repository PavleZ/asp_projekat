using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTO.LogDTOs
{
    public class GetLogsDTO
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string UseCase { get; set; }

        public UserDTO  User { get; set; }



        public DateTime CreatedAt { get; set; }




    }
}
