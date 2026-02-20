
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
namespace TaskManager.DTOs;

public class TaskCreateDto
{
    [Required(ErrorMessage = "Title is required")]
    public required string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}
