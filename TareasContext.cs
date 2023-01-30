using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using proyectoef;

namespace proyectoef.Models
{
    public class TareasContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        // Añadiendo restricciones y validaciones con FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Categoria> categoriasInit = new List<Categoria>();

            categoriasInit.Add(new Categoria() { 
                CategoriaId = Guid.Parse("3a82ce26-8edd-4c72-9b05-05872b781198"), 
                Nombre = "Actividades pendientes", 
                Peso = 20 
            });

            categoriasInit.Add(new Categoria() { 
                CategoriaId = Guid.Parse("3a82ce26-8edd-4c72-9b05-05872b781102"), 
                Nombre = "Actividades persoanles", 
                Peso = 50 
            });

            //Configuración modelo categorias con FluentAPI
            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).HasMaxLength(200);

                categoria.Property(p => p.Peso);

                // Agregando datos semilla
                categoria.HasData(categoriasInit);
            });


            //Configuración modelo Tareas con FluentAPI
            List<Tarea> tareasInit = new List<Tarea>();

            tareasInit.Add(new Tarea() { 
                TareaId = Guid.Parse("2bf073d5-fb9b-4081-b602-803678782d7c"), 
                CategoriaId = Guid.Parse("3a82ce26-8edd-4c72-9b05-05872b781198"),
                PrioridadTarea = Prioridad.Media,
                Titulo = "Pago de servicios publicos",
                FechaCreacion = DateTime.Now
            });

            tareasInit.Add(new Tarea() { 
                TareaId = Guid.Parse("f0de7f3b-50cf-4245-857e-7f13f13012e8"), 
                CategoriaId = Guid.Parse("3a82ce26-8edd-4c72-9b05-05872b781102"),
                PrioridadTarea = Prioridad.Baja,
                Titulo = "Terminar de ver serie",
                FechaCreacion = DateTime.Now
            });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);

                tarea
                .HasOne(p => p.Categoria)
                .WithMany(p => p.Tareas)
                .HasForeignKey(p => p.CategoriaId);

                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion);
                tarea.Property(p => p.FechaCreacion);

                tarea.Ignore(p => p.Resumen);

                tarea.Property(p => p.PrioridadTarea).HasConversion(
                    v => v.ToString(),
                    v => (Prioridad)Enum.Parse(typeof(Prioridad), v));

                tarea.Property(p => p.Autor).HasMaxLength(150);

                tarea.HasData(tareasInit);
            });
        }
    }
}