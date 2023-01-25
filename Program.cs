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

// Todas las tareas
app.MapGet("/api/tareas", ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas);
});

// Trae categorías
app.MapGet("/api/categorias", ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Categorias);
});

// Obteniendo datos por prioridad baja
app.MapGet("/api/lowPriority", async ([FromServices] TareasContext dbContext) =>
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

// Guardando datos POST
app.MapPost("api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) => 
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;

    await dbContext.AddAsync(tarea);
    // await dbContext.Tareas.AddAsync(tarea);

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
