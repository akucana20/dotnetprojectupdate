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
        private readonly IUserService _userService; 
        private readonly IProjectService _projectService; 
        public AdminController(IUserService userService, IProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
        }

        [HttpPost("user")]
        public IActionResult CreateUser([FromBody] CreateUserModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userService.CreateUser(model);

            if (user != null)
                return Ok(user);
            else
                return BadRequest();
        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = _userService.UpdateUser(userId, model);

            if (updatedUser != null)
                return Ok(updatedUser);
            else
                return NotFound();
        }

        [HttpDelete("user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (_userService.DeleteUser(userId))
                return Ok();
            else
                return NotFound();
        }

        
        [HttpPost("project/{projectId}/employee/{employeeId}")]
        public IActionResult AddEmployeeToProject(int projectId, int employeeId)
        {
            if (_projectService.AddEmployeeToProject(projectId, employeeId))
                return Ok();
            else
                return NotFound();
        }

       
        [HttpDelete("project/{projectId}/employee/{employeeId}")]
        public IActionResult RemoveEmployeeFromProject(int projectId, int employeeId)
        {
           
            if (_projectService.RemoveEmployeeFromProject(projectId, employeeId))
                return Ok();
            else
                return NotFound();
        }

    }
}
