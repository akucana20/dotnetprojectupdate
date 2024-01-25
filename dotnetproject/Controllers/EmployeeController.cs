using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models; // Updated to the correct namespace
using dotnetproject.Services; // Updated to the correct namespace
// Other using statements

namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Employee")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService; // Assume an employee service for database operations

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Endpoint to update employee profile
        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] EmployeeProfileUpdateModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Update the profile using the employee service
            var result = _employeeService.UpdateProfile(User.Identity.Name, model);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        // Endpoint to get employee profile
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Retrieve profile using the employee service
            var profile = _employeeService.GetProfile(User.Identity.Name);

            if (profile != null)
                return Ok(profile);
            else
                return NotFound();
        }

        // Endpoint to create a task
        [HttpPost("task")]
        public IActionResult CreateTask([FromBody] CreateTaskModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Create the task using the employee service
            var createdTask = _employeeService.CreateTask(User.Identity.Name, model);

            if (createdTask != null)
                return Ok(createdTask);
            else
                return BadRequest();
        }

        // Endpoint to mark a task as completed
        [HttpPut("task/{taskId}/complete")]
        public IActionResult CompleteTask(int taskId)
        {
            // Mark the task as completed using the employee service
            var result = _employeeService.CompleteTask(User.Identity.Name, taskId);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        // Endpoint to view tasks
        [HttpGet("tasks")]
        public IActionResult GetTasks()
        {
            // Retrieve tasks using the employee service
            var tasks = _employeeService.GetTasks(User.Identity.Name);

            return Ok(tasks);
        }

    }
}
