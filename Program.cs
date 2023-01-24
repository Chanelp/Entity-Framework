using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

// Configuraciòn para conectar a bd sql server
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cntareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World desde sql server!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => {
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos sql server string en appsettings: " + dbContext.Database.IsSqlServer());
});

app.Run();
