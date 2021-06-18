using Blog.Application;
using Blog.Application.Commands.PostCommands;
using Blog.Application.DTO.PostDTOs;
using Blog.Application.Queries;
using Blog.Application.Searches;
using Microsoft.AspNetCore.Http;
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
    public class PostController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public PostController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch search,
            [FromServices] IGetPostsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));

        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetPostQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));

        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post([FromForm] CreatePostDTO dto, [FromServices] ICreatePostCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UpdatePostDTO dto, [FromServices] IUpdatePostCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePostCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
