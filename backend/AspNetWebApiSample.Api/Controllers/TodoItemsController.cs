using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetWebApiSample.Api.Models;

namespace AspNetWebApiSample.Api.Controllers
{
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
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItemDto()
        {
            return await _context.TodoItemDto.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItemDto(long id)
        {
            var todoItemDto = await _context.TodoItemDto.FindAsync(id);

            if (todoItemDto == null)
            {
                return NotFound();
            }

            return todoItemDto;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemDto(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItemDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemDtoExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItemDto(TodoItemDto todoItemDto)
        {
            _context.TodoItemDto.Add(todoItemDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItemDto", new { id = todoItemDto.Id }, todoItemDto);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemDto(long id)
        {
            var todoItemDto = await _context.TodoItemDto.FindAsync(id);
            if (todoItemDto == null)
            {
                return NotFound();
            }

            _context.TodoItemDto.Remove(todoItemDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemDtoExists(long id)
        {
            return _context.TodoItemDto.Any(e => e.Id == id);
        }
    }
}
