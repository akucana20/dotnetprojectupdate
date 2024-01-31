using System.Collections.Generic;
using System.Linq;
using dotnetproject.Models; // Replace with your actual models namespace
using dotnetproject.Services;
using dotnetproject.Data; // Replace with the namespace of your DbContext

namespace dotnetproject.Services
{
    public interface ITaskService
    {
        Task CreateTask(CreateTaskModel model);
        Task UpdateTask(int taskId, UpdateTaskModel model);
        Task GetTaskById(int taskId);
        IEnumerable<Task> GetAllTasks();
        bool AssignTask(int taskId, int employeeId);
        bool DeleteTask(int taskId);
       
    }

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateTask(CreateTaskModel model)
        {
            var task = new Task
            {
                Name = model.Name,
                Description = model.Description,
                Status = TaskStatus.Pending,
                AssignedToEmployeeId = model.AssignedToEmployeeId,
                ProjectId = model.ProjectId,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        public Task UpdateTask(int taskId, UpdateTaskModel model)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            task.Name = model.Name;
            task.Description = model.Description;
            task.Status = model.Status;
            task.AssignedToEmployeeId = model.AssignedToEmployeeId;
            task.StartDate = model.StartDate;
            task.EndDate = model.EndDate;

            _context.SaveChanges();

            return task;
        }

        public Task GetTaskById(int taskId)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public bool AssignTask(int taskId, int employeeId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return false;
            }

            task.AssignedToEmployeeId = employeeId;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteTask(int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return true;
        }

    }
}
