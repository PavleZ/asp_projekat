using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.PostCommands
{
    public interface IDeletePostCommand : ICommand<int>
    {
    }
}
