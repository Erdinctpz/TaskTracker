using TaskTracker.Application.DTOs;

namespace TaskTracker.Application.Abstract
{
    public interface IGroupService
    {
        Task<Result<List<GroupDto>>> GetAllAsync(string userId);
    }
}