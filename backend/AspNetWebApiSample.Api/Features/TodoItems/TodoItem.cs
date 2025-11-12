using Riok.Mapperly.Abstractions;

namespace AspNetWebApiSample.Api.Features.TodoItems;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    [MapperIgnore]
    public string? Secret { get; set; }
}
