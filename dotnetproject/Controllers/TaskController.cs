using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models; // Replace with your actual models namespace
using dotnetproject.Services; // Replace with your actual services namespace
// Other using statements

namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Administrator,Employee")] // Adjust roles as needed
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Endpoint to create a new task
        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to create a task
            var task = _taskService.CreateTask(model);

            if (task != null)
                return Ok(task);
            else
                return BadRequest();
        }

        // Endpoint to update a task
        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int taskId, [FromBody] UpdateTaskModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to update a task
            var updatedTask = _taskService.UpdateTask(taskId, model);

            if (updatedTask != null)
                return Ok(updatedTask);
            else
                return NotFound();
        }

        // Endpoint to get a task by id
        [HttpGet("{taskId}")]
        public IActionResult GetTaskById(int taskId)
        {
            // Implementation to get a task by id
            var task = _taskService.GetTaskById(taskId);

            if (task != null)
                return Ok(task);
            else
                return NotFound();
        }

        // Endpoint to get all tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            // Implementation to get all tasks
            var tasks = _taskService.GetAllTasks();

            return Ok(tasks);
        }

        // Endpoint to assign a task
        [HttpPut("{taskId}/assign/{employeeId}")]
        public IActionResult AssignTask(int taskId, int employeeId)
        {
            // Implementation to assign a task
            if (_taskService.AssignTask(taskId, employeeId))
                return Ok();
            else
                return NotFound();
        }

        // Endpoint to delete a task
        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            // Implementation to delete a task
            if (_taskService.DeleteTask(taskId))
                return Ok();
            else
                return NotFound();
        }

        // Other endpoints as needed...
    }
}
