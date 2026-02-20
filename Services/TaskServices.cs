using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly TaskContext _db;

    public TaskService(TaskContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _db.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _db.Tasks.FindAsync(id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
{
    ArgumentNullException.ThrowIfNull(task);

    if (string.IsNullOrWhiteSpace(task.Title))
       throw new InvalidOperationException("Task title cannot be empty");

    _db.Tasks.Add(task);
    await _db.SaveChangesAsync();

    return task;
}

    public async Task<TaskItem> UpdateAsync(int id, TaskItem updatedTask)
    {
        ArgumentNullException.ThrowIfNull(updatedTask);

        var task = await _db.Tasks.FindAsync(id)
            ?? throw new KeyNotFoundException("Task not found");

        if (string.IsNullOrWhiteSpace(updatedTask.Title))
            throw new ArgumentException("Task title cannot be empty", nameof(updatedTask));

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        await _db.SaveChangesAsync();

        return task;
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id)
            ?? throw new KeyNotFoundException("Task not found");

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
    }

}
