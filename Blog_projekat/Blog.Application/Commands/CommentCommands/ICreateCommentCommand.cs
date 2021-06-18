using Blog.Application.DTO.CommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.CommentCommands
{
    public interface ICreateCommentCommand : ICommand<CreateCommentDTO>
    {
    }
}
