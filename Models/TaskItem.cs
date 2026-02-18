namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } // adiciona aqui
    public bool IsCompleted { get; set; }
}