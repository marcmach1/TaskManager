
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

public class TaskCreateDto
{
      [Required(ErrorMessage = "Title is required")]
     public required string Title { get; set; }  
}