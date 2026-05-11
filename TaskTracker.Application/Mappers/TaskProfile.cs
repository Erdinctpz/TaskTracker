using AutoMapper;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain;

namespace TaskTracker.Application.Mappers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TodoItem, TaskDto>();
            CreateMap<CreateTaskDto, TodoItem>();
        }
    }
}