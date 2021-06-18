using Blog.Application;
using Blog.Application.Commands.UserCommands;
using Blog.Application.DTO.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RegisterController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // POST api/<RegisterController>
        [HttpPost]
        public void Post([FromBody] UserRegisterDTO dto, [FromServices] IRegisterUserCommand command)
        {

            _executor.ExecuteCommand(command, dto);
        }

       
    }
}
