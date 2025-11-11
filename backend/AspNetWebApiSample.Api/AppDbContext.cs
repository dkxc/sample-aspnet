using Microsoft.EntityFrameworkCore;
using AspNetWebApiSample.Api.Models;

namespace AspNetWebApiSample.Api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { 
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

public DbSet<AspNetWebApiSample.Api.Models.TodoItemDto> TodoItemDto { get; set; } = default!;
}
