using System.ComponentModel.DataAnnotations;


namespace TaskManager.DTOs
{
    public class TaskReadDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }  
        public bool IsCompleted { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}