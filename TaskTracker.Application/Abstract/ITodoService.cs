using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain;

namespace TaskTracker.Application.Abstract
{
    public interface ITodoService
    {
        Task<Result<List<TaskDto>>> GetAllAsync(string userId);
        Task<Result<TaskDto>> InsertAsync(CreateTaskDto createTaskDto, string userId);
    }
}