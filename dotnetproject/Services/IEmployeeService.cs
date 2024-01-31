using dotnetproject.Models;
using System.Collections.Generic;

namespace dotnetproject.Services
{
    public interface IEmployeeService
    {
        bool UpdateProfile(string username, EmployeeProfileUpdateModel model);

        Task CreateTask(string username, CreateTaskModel model);

        bool CompleteTask(string username, int taskId);
        Employee GetProfile(string username);


        IEnumerable<Task> GetTasks(string username);
    }
}
