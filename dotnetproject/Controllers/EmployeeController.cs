using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models; 
using dotnetproject.Services; 


namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Employee")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService; 
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] EmployeeProfileUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var result = _employeeService.UpdateProfile(User.Identity.Name, model);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
           
            var profile = _employeeService.GetProfile(User.Identity.Name);

            if (profile != null)
                return Ok(profile);
            else
                return NotFound();
        }

      
        [HttpPost("task")]
        public IActionResult CreateTask([FromBody] CreateTaskModel model)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var createdTask = _employeeService.CreateTask(User.Identity.Name, model);

            if (createdTask != null)
                return Ok(createdTask);
            else
                return BadRequest();
        }

     
        [HttpPut("task/{taskId}/complete")]
        public IActionResult CompleteTask(int taskId)
        {
           
            var result = _employeeService.CompleteTask(User.Identity.Name, taskId);

            if (result)
                return Ok();
            else
                return NotFound();
        }

      
        [HttpGet("tasks")]
        public IActionResult GetTasks()
        {
            
            var tasks = _employeeService.GetTasks(User.Identity.Name);

            return Ok(tasks);
        }

    }
}
