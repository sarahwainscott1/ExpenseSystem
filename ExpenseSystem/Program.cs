using ExpenseSystem.Models;
using Microsoft.EntityFrameworkCore;
using ExpenseSystem.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")); });
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();

