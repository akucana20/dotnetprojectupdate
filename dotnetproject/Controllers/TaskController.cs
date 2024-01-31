using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models; 
using dotnetproject.Services; 


namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Administrator,Employee")] 
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

      
        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskModel model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var task = _taskService.CreateTask(model);

            if (task != null)
                return Ok(task);
            else
                return BadRequest();
        }

     
        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int taskId, [FromBody] UpdateTaskModel model)
        {
          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

        
            var updatedTask = _taskService.UpdateTask(taskId, model);

            if (updatedTask != null)
                return Ok(updatedTask);
            else
                return NotFound();
        }

   
        [HttpGet("{taskId}")]
        public IActionResult GetTaskById(int taskId)
        {
            
            var task = _taskService.GetTaskById(taskId);

            if (task != null)
                return Ok(task);
            else
                return NotFound();
        }

       
        [HttpGet]
        public IActionResult GetAllTasks()
        {
        
            var tasks = _taskService.GetAllTasks();

            return Ok(tasks);
        }

      
        [HttpPut("{taskId}/assign/{employeeId}")]
        public IActionResult AssignTask(int taskId, int employeeId)
        {
           
            if (_taskService.AssignTask(taskId, employeeId))
                return Ok();
            else
                return NotFound();
        }


        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
        
            if (_taskService.DeleteTask(taskId))
                return Ok();
            else
                return NotFound();
        }

        
    }
}
