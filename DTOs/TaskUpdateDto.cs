using System.ComponentModel.DataAnnotations;

// Para atualizar uma task
public class TaskUpdateDto
{

     [Required(ErrorMessage = "Title is required")]
    public required string Title { get; set; }  
    public bool IsCompleted { get; set; }
}