using System.Linq.Expressions;
using AutoMapper;
using TaskTracker.Application.Abstract;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain.Entities;
using Name;

namespace TaskTracker.Application.Services
{
    public class GroupService(
        IGenericRepository<TaskGroup> repository,
        IMapper mapper
    ) : IGroupService
    {
        public async Task<Result<List<GroupDto>>> GetAllAsync(string userId)
        {
            Expression<Func<TaskGroup, bool>>? predicate = g => g.UserGroups != null && g.UserGroups.Any(ug => ug.UserId == userId);

            var groupList = await repository.GetAllAsync(predicate);

            var mappedList = mapper.Map<List<GroupDto>>(groupList);

            return Result<List<GroupDto>>.Success(mappedList);
        }
    }
}