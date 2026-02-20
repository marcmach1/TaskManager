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
    public async Task<IEnumerable<TaskReadDto>> Get()
    {
        var tasks = await _service.GetAllAsync();
        return tasks.Select(t => t.ToReadDto());
    }

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDto>> Get(int id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task.ToReadDto());
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> Post([FromBody] TaskCreateDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required.");

        try
        {
            var task = dto.ToTaskItem();
            var created = await _service.CreateAsync(task);

            return CreatedAtAction(
                nameof(Get),
                new { id = created.Id },
                created.ToReadDto());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TaskUpdateDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid data.");

        try
        {
            var task = dto.ToTaskItem();
            await _service.UpdateAsync(id, task);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
