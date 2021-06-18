using Blog.Application;
using Blog.Application.Commands.CommentCommands;
using Blog.Application.DTO.CommentDTOs;
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
    public class CommentController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public CommentController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public IActionResult Get([FromQuery] CommentSearch search,
            [FromServices] IGetCommentsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));

        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCommentQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));

        }

        // POST api/<CommentController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDTO dto, [FromServices] ICreateCommentCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCommentDTO dto, [FromServices] IUpdateCommentCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
