using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=taskmanager.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
