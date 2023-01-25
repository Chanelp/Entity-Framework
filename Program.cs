using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuraciòn para conectar a bd sql server
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cntareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World desde sql server!");

// Endpoints
app.MapGet("/dbconexion", ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos sql server string en appsettings: " + dbContext.Database.IsSqlServer());
});

app.MapGet("/api/tareas", ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Where(t => t.PrioridadTarea == Prioridad.Baja));
});

app.MapGet("/api/categorias", ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Categorias);
});

app.MapGet("/api/tasks", async ([FromServices] TareasContext dbContext) =>
{
    var data = dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == Prioridad.Baja);
    return Results.Ok(data);
});

// Un pequeño ejemplo de cómo sería una API para filtrar información en base a la prioridad
app.MapGet("/api/task/priority/{id}", async ([FromServices] TareasContext dbContext, int id) =>
{
    var data = dbContext.Tareas.Include(a => a.Categoria).Where(a => (int)a.PrioridadTarea == id);
    return Results.Ok(data);
});

app.Run();
