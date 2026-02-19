using TaskManager.Models;
using TaskManager.DTOs;
using System;

namespace TaskManager.Extensions
{
    public static class TaskItemExtensions
    {
        public static TaskReadDto ToReadDto(this TaskItem task)
        {
            return new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted
            };
        }

        public static TaskItem ToTaskItem(this TaskCreateDto dto)
        {
            return new TaskItem
            {
                Title = dto.Title,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(this TaskItem task, TaskUpdateDto dto)
        {
            task.Title = dto.Title;
            task.IsCompleted = dto.IsCompleted;
        }
    }
}
