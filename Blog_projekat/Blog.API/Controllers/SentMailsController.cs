using Blog.Application;
using Blog.Application.Queries;
using Blog.Application.Searches;
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
    public class SentMailsController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public SentMailsController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }



        // GET: api/<SentMailsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SentMailSearch search,
            [FromServices] IGetSentMailsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));

        }

        // GET api/<SentMailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSentMailQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

      
    }
}
