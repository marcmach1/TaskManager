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

    public async Task<List<TaskItem>> GetAllAsync() =>
        await _db.Tasks.ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _db.Tasks.FindAsync(id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(int id, TaskItem updatedTask)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null) return false;

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null) return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }
}
