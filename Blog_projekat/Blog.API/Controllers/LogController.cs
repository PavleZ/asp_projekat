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
    public class LogController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public LogController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<LogController>
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearch search,
            [FromServices] IGetLogsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));

        }

        // GET api/<LogController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetLogQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));

        }


    }
}
