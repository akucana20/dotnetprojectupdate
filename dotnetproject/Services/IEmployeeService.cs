using dotnetproject.Models;
using System.Collections.Generic;

namespace dotnetproject.Services
{
    public interface IEmployeeService
    {
        // Updates the profile of an employee based on the provided username and update model
        bool UpdateProfile(string username, EmployeeProfileUpdateModel model);

        // Creates a new task based on the provided model
        Task CreateTask(string username, CreateTaskModel model);

        // Marks a task as completed based on the provided task ID
        bool CompleteTask(string username, int taskId);
        Employee GetProfile(string username);


        // Retrieves a list of tasks assigned to an employee
        IEnumerable<Task> GetTasks(string username);
    }
}
