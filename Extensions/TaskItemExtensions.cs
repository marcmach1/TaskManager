using TaskManager.Models;
using TaskManager.DTOs;

namespace TaskManager.Extensions;

public static class TaskItemExtensions
{
    public static TaskItem ToTaskItem(this TaskCreateDto dto)
    {
        return new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false
        };
    }

    public static TaskItem ToTaskItem(this TaskUpdateDto dto)
    {
        return new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted
        };
    }

    public static TaskReadDto ToReadDto(this TaskItem task)
    {
        return new TaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted
        };
    }
}
