using AspNetWebApiSample.Api.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetWebApiSample.Api.Features.TodoItems;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        return await _context.Set<TodoItem>()
            .Select(item => TodoItemMapper.ToDto(item))
            .ToListAsync();
    }

    // GET: api/TodoItems/id
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        var todoItem = await _context.Set<TodoItem>().FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return TodoItemMapper.ToDto(todoItem);
    }

    // PUT: api/TodoItems/id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItemDto)
    {
        if (id != todoItemDto.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.Set<TodoItem>().FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        TodoItemMapper.UpdateEntity(todoItemDto, todoItem);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/TodoItems
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDto)
    {
        var todoItem = TodoItemMapper.ToEntity(todoItemDto);

        _context.Set<TodoItem>().Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            TodoItemMapper.ToDto(todoItem));
    }

    // DELETE: api/TodoItems/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.Set<TodoItem>().FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.Set<TodoItem>().Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id)
    {
        return _context.Set<TodoItem>().Any(e => e.Id == id);
    }
}
