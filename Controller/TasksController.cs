using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskItem>> Get() =>
        await _service.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> Get(int id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null) return NotFound();
        return task;
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Post([FromBody] TaskItem task)
    {
        if (task == null || string.IsNullOrEmpty(task.Title))
            return BadRequest("Title is required.");

        var created = await _service.CreateAsync(task);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TaskItem updatedTask)
    {
        var updated = await _service.UpdateAsync(id, updatedTask);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
