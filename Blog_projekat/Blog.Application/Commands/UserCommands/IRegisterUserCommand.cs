﻿using Blog.Application.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Commands.UserCommands
{
    public interface IRegisterUserCommand:ICommand<UserRegisterDTO>
    {
    }
}
