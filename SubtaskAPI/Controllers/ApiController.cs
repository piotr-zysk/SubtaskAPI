using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubtaskAPI.Logic;
using SubtaskAPI.Models;

namespace SubtaskAPI.Controllers
{
    [Route("[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ITaskLogic _logic;

        public ApiController(ITaskLogic logic)
        {
            this._logic = logic;
        }

        // GET api/values
        [HttpGet("db")]
        public ActionResult<IEnumerable<TaskItem>> GetDb()
        {
            var x = _logic.GetAllTaskItems();
            return Ok(x);
        }

        [HttpGet]
        public ActionResult<TaskEntityState> GetFullTasks()
        {
            TaskEntityState x;
            try
            {
                x = _logic.GetAllTasks();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Oops. Database Error Occured. :( Please try again much later.\r\n"+e.Message);
            }

            return Ok(x);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

