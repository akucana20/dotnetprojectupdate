using System.Collections.Generic;
using System.Linq;
using dotnetproject.Models; // Updated to the correct namespace
using dotnetproject.Data; // Updated to the correct namespace
using dotnetproject.Services;
using Microsoft.EntityFrameworkCore;




namespace dotnetproject.Services
{
    public interface IProjectService
    {
        Project CreateProject(CreateProjectModel model);
        Project UpdateProject(int projectId, UpdateProjectModel model);
        Project GetProjectById(int projectId);
        IEnumerable<Project> GetAllProjects();
        bool DeleteProject(int projectId);
        bool AddEmployeeToProject(int projectId, int employeeId);
        bool RemoveEmployeeFromProject(int projectId, int employeeId);
    }

    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project CreateProject(CreateProjectModel model)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Employees = new List<Employee>() // Initialize empty employee collection
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        public Project UpdateProject(int projectId, UpdateProjectModel model)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            project.Name = model.Name;
            project.Description = model.Description;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;

            _context.SaveChanges();

            return project;
        }

        public Project GetProjectById(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == projectId);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public bool DeleteProject(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return true;
        }

        public bool AddEmployeeToProject(int projectId, int employeeId)
        {
            var project = _context.Projects.Include(p => p.Employees).FirstOrDefault(p => p.Id == projectId);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == employeeId);

            if (project == null || employee == null || project.Employees.Contains(employee))
            {
                return false;
            }

            project.Employees.Add(employee);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            var project = _context.Projects.Include(p => p.Employees).FirstOrDefault(p => p.Id == projectId);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == employeeId);

            if (project == null || employee == null || !project.Employees.Contains(employee))
            {
                return false;
            }

            project.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }

        // Implement other methods as needed...
    }
}
