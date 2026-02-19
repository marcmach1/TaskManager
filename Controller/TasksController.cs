using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.DTOs;
using TaskManager.Extensions;

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

    // GET: api/tasks
    [HttpGet]
    public async Task<IEnumerable<TaskReadDto>> Get() =>
        (await _service.GetAllAsync()).Select(t => t.ToReadDto());

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDto>> Get(int id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null) return NotFound();
        return task.ToReadDto();
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> Post([FromBody] TaskCreateDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.Title))
            return BadRequest("Title is required.");

        var task = dto.ToTaskItem();                  // converte DTO para Model
        var created = await _service.CreateAsync(task);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToReadDto());
    }

    // PUT: api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDto dto)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null) return NotFound();

        task.UpdateFromDto(dto);                      // atualiza Model com DTO
        var updated = await _service.UpdateAsync(id, task);
        if (!updated) return NotFound();

        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
