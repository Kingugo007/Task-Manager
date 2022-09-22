using AutoMapper;
using Microsoft.Extensions.Configuration;
using TaskManager.Core.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly IConfiguration _config;
        private readonly IHttpCommandHandler _httpCommandHandler;
        private readonly IMapper _mapper;
        private readonly string baseUrl = "https://632b03111090510116ce8c28.mockapi.io/api/v1/Tasks";
        public TaskServices(IConfiguration config,
            IHttpCommandHandler httpCommandHandler,
            IMapper mapper
            )
        {
            _config = config;
            _httpCommandHandler = httpCommandHandler;
            _mapper = mapper;   
        }
        public async Task<IList<TaskListDTO>> GetAllTasksAsync()
        {
            var tasks = await _httpCommandHandler.GetRequest<List<TaskList>>(baseUrl);
            var result = _mapper.Map<IList<TaskListDTO>>(tasks);
            foreach (var task in result)
            {
                task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
                task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
                task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
                task.DaysLate =  task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
            }
            return result;
        }
        public async Task<TaskList> AddTaskAsync(CreateTaskDTO data)
        {
          var model = _mapper.Map<TaskList>(data);
          model.StartDate = DateTime.Now;
          var res = await _httpCommandHandler.PostRequest<TaskList, TaskList>(model, baseUrl);
          return res;
        }

        public async Task<TaskListDTO> GetTaskByIdAsync(string Id)
        {
            string url = $"{baseUrl}/{Id}";
            var task = await _httpCommandHandler.GetRequest<TaskListDTO>(url);
            task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
            task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
            task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
            task.DaysLate = task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
            return task;
        }
        public async Task<TaskList> DeleteAsync(string Id)
        {
            string url = $"{baseUrl}/{Id}";
            return await _httpCommandHandler.DeleteRequest<TaskList>(url);
        }
    }
}
