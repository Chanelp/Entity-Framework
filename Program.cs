using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

// Configuraciòn para conectar a bd en memoria
// builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDb"));

// Configuraciòn para conectar a bd sql server
builder.Services.AddSqlServer<TareasContext>("Data Source=AD-ASTRA;initial Catalog=TareasDb;user id=sa;password=micuentabd27;");

var app = builder.Build();

app.MapGet("/", () => "Hello World desde sql server!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => {
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos sql server: " + dbContext.Database.IsInMemory());
});

app.Run();
