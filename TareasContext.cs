using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef
{
    public class TareasContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuración modelo categorias con FluentAPI
            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

                categoria.Property(p => p.Descripcion).IsRequired().HasMaxLength(200);
            });

            //Configuración modelo Tareas con FluentAPI
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
            });
        }
    }
}