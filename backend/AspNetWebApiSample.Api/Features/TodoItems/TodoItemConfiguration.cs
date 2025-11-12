using Microsoft.EntityFrameworkCore;

namespace AspNetWebApiSample.Api.Features.TodoItems;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.IsComplete).IsRequired();
        builder.Property(t => t.Secret).HasMaxLength(200);
    }
}
