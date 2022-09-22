using AutoMapper;
using TaskManager.Core.DTOs;
using TaskManager.Domain.Models;

namespace TaskManager.Core
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<TaskList, TaskListDTO>().ReverseMap();
            CreateMap<TaskList, CreateTaskDTO>().ReverseMap();
        }
    }
}
