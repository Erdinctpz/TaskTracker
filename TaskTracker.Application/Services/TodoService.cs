using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using Name;
using TaskTracker.Application.Abstract;
using TaskTracker.Application.DTOs;
using TaskTracker.Domain;


namespace TaskTracker.Application.Services
{
    public class TodoService(
        IGenericRepository<TodoItem> repository,
        IMapper mapper,
        IValidator<CreateTaskDto> createTaskDtoValidator
    ) : ITodoService
    {
        public async Task<Result<List<TaskDto>>> GetAllAsync(Expression<Func<TodoItem, bool>>? predicate)
        {
            var taskList = await repository.GetAllAsync(predicate);

            var mappedList = mapper.Map<List<TaskDto>>(taskList);

            return Result<List<TaskDto>>.Success(mappedList);
        }

        public async Task<Result<TaskDto>> InsertAsync(CreateTaskDto createTaskDto, string userId)
        {
            var validationResult = await createTaskDtoValidator.ValidateAsync(createTaskDto);

            if (!validationResult.IsValid)
            {
                var errorMsg = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Alanları kontrol ediniz.";
                return Result<TaskDto>.Failure(errorMsg);
            }

            var entity = mapper.Map<TodoItem>(createTaskDto);
            entity.UserId = userId;

            await repository.InsertAsync(entity);

            var mapped = mapper.Map<TaskDto>(entity);
            return Result<TaskDto>.Success(mapped);
        }
    }
}