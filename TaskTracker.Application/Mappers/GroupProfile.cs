using AutoMapper;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappers
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<TaskGroup, GroupDto>();
        }
    }
}