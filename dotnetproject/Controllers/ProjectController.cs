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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // Endpoint to create a new project
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to create a project
            var project = _projectService.CreateProject(model);

            if (project != null)
                return Ok(project);
            else
                return BadRequest();
        }

        // Endpoint to update a project
        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] UpdateProjectModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Implementation to update a project
            var updatedProject = _projectService.UpdateProject(projectId, model);

            if (updatedProject != null)
                return Ok(updatedProject);
            else
                return NotFound();
        }

        // Endpoint to get a project by id
        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(int projectId)
        {
            // Implementation to get a project by id
            var project = _projectService.GetProjectById(projectId);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }

        // Endpoint to get all projects
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            // Implementation to get all projects
            var projects = _projectService.GetAllProjects();

            return Ok(projects);
        }

        // Endpoint to delete a project
        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            // Implementation to delete a project
            if (_projectService.DeleteProject(projectId))
                return Ok();
            else
                return NotFound();
        }

        // Other endpoints as needed...
    }
}
