using AspNetWebApiSample.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "TestCors", policy =>
    {
        policy.WithOrigins(["https://localhost:5173", "http://localhost:5173", "https://127.0.0.1:5173", "http://127.0.0.1:5173"])
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("TestCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
