using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BL.Abstraction;
using TaskManager.Core.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        // GET: api/<controller/1/all>
        [HttpGet("{userId}/all")]
        public async Task<IActionResult> Get(string userId)
        {
            var result = await _taskService.GetAllTasksByUserId(userId);
            return Ok(result);
        }

        // GET: api/<controller/1>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> Get(int taskId)
        {
            var result = await _taskService.GetTaskAsync(taskId);
            if (result == null)
            {
                return NotFound($"Task with id - {taskId} not found");
            }
            return Ok(result);
        }
        // POST api/<controller>
        [HttpPost("{userId}")]
        public async Task<IActionResult> Post(string userId, [FromBody]TaskModelDto model)
        {
            await _taskService.CreateNewTaskAsync(userId, model);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId, [FromBody]TaskModelDto model)
        {
            await _taskService.UpdateTaskAsync(userId, model);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            await _taskService.DeleteTaskAsync(taskId);
            return Ok();
        }
    }
}
