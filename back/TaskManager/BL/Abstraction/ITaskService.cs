using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Models.DTO;

namespace TaskManager.BL.Abstraction
{
    public interface ITaskService : IDisposable
    {
        Task CreateNewTaskAsync(string userId,TaskModelDto model);
        Task<TaskModelDto> GetTaskAsync(int taskId);
        Task<IEnumerable<TaskModelDto>> GetAllTasksByUserId(string userId);
        Task DeleteTaskAsync(int taskId);
        Task UpdateTaskAsync(string userId, TaskModelDto model);
    }
}
