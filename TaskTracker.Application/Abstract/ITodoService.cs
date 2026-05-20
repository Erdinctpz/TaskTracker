using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain;

namespace TaskTracker.Application.Abstract
{
    public interface ITodoService
    {
        Task<Result<List<TaskDto>>> GetAllAsync(Expression<Func<TodoItem, bool>>? predicate);
        Task<Result<TaskDto>> InsertAsync(CreateTaskDto createTaskDto, string userId);
    }
}