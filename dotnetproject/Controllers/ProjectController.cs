using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetproject.Models;
using dotnetproject.Services;

namespace dotnetproject.Controllers
{
    [Authorize(Roles = "Administrator,Employee")]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = _projectService.CreateProject(model);

            if (project != null)
                return Ok(project);
            else
                return BadRequest();
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] UpdateProjectModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProject = _projectService.UpdateProject(projectId, model);

            if (updatedProject != null)
                return Ok(updatedProject);
            else
                return NotFound();
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(int projectId)
        {
            var project = _projectService.GetProjectById(projectId);

            if (project != null)
                return Ok(project);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            if (_projectService.DeleteProject(projectId))
                return Ok();
            else
                return NotFound();
        }

  
    }
}
