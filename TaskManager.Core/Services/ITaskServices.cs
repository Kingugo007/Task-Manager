using TaskManager.Core.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Services
{
    public interface ITaskServices
    {
        Task<IList<TaskListDTO>> GetAllTasksAsync();
        Task<TaskList> AddTaskAsync(CreateTaskDTO data);
        Task<TaskListDTO> GetTaskByIdAsync(string Id);
        Task<TaskList> DeleteAsync(string Id);
    }
}