using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models;
using dotnetproject.Services;


namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService; // User management service
        private readonly IProjectService _projectService; // Project management service

        public AdminController(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
        }

        // Endpoint to create a new user
        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to create a user
            var user = _userService.CreateUser(model);

            if (user != null)
                return Ok(user);
            else
                return BadRequest();
        }

        // Endpoint to update a user
        [HttpPut("user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UpdateUserModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to update a user
            var updatedUser = _userService.UpdateUser(userId, model);

            if (updatedUser != null)
                return Ok(updatedUser);
            else
                return NotFound();
        }

        // Endpoint to delete a user
        [HttpDelete("user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            // Implementation to delete a user
            if (_userService.DeleteUser(userId))
                return Ok();
            else
                return NotFound();
        }

        // Endpoint to add an employee to a project
        [HttpPost("project/{projectId}/employee/{employeeId}")]
        public IActionResult AddEmployeeToProject(int projectId, int employeeId)
        {
            // Implementation to add an employee to a project
            if (_projectService.AddEmployeeToProject(projectId, employeeId))
                return Ok();
            else
                return NotFound();
        }

        // Endpoint to remove an employee from a project
        [HttpDelete("project/{projectId}/employee/{employeeId}")]
        public IActionResult RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            // Implementation to remove an employee from a project
            if (_projectService.RemoveEmployeeFromProject(projectId, employeeId))
                return Ok();
            else
                return NotFound();
        }

        // Other endpoints as needed...
    }
}
