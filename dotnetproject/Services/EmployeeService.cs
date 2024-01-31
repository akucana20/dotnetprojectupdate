using dotnetproject.Data;
using dotnetproject.Models;
using System.Linq;

namespace dotnetproject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool UpdateProfile(string username, EmployeeProfileUpdateModel model)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.User.Username == username);
            if (employee == null) return false;

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Position = model.Position;
            employee.Department = model.Department;

            _context.SaveChanges();
            return true;
        }

        public Task CreateTask(string username, CreateTaskModel model)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == model.AssignedToEmployeeId);
            if (employee == null || employee.User.Username != username) return null;

            var task = new Task
            {
                Name = model.Name,
                Description = model.Description,
                AssignedToEmployeeId = model.AssignedToEmployeeId,
                ProjectId = model.ProjectId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        public bool CompleteTask(string username, int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId && t.AssignedToEmployee.User.Username == username);
            if (task == null) return false;

            _context.SaveChanges();
            return true;
        }
        public Employee GetProfile(string username)
        {
            return _context.Employees.FirstOrDefault(e => e.User.Username == username);
        }


        public IEnumerable<Task> GetTasks(string username)
        {
            return _context.Tasks
                .Where(t => t.AssignedToEmployee.User.Username == username)
                .ToList();
        }
    }
}
