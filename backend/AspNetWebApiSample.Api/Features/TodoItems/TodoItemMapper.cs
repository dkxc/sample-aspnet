using Riok.Mapperly.Abstractions;

namespace AspNetWebApiSample.Api.Features.TodoItems;

[Mapper]
public partial class TodoItemMapper
{
    // GET
    public static partial TodoItemDto ToDto(TodoItem todoItem);

    // POST
    public static partial TodoItem ToEntity(TodoItemDto todoItemDto);

    // PUT
    public static partial void UpdateEntity(TodoItemDto todoItemDto, TodoItem todoItem);
}
