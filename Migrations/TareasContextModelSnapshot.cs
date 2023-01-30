﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using proyectoef.Models;

#nullable disable

namespace proyectoef.Migrations
{
    [DbContext(typeof(TareasContext))]
    partial class TareasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("proyectoef.Models.Categoria", b =>
                {
                    b.Property<Guid>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria", (string)null);

                    b.HasData(
                        new
                        {
                            CategoriaId = new Guid("3a82ce26-8edd-4c72-9b05-05872b781198"),
                            Nombre = "Actividades pendientes",
                            Peso = 20
                        },
                        new
                        {
                            CategoriaId = new Guid("3a82ce26-8edd-4c72-9b05-05872b781102"),
                            Nombre = "Actividades persoanles",
                            Peso = 50
                        });
                });

            modelBuilder.Entity("proyectoef.Models.Tarea", b =>
                {
                    b.Property<Guid>("TareaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Autor")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrioridadTarea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("TareaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Tarea", (string)null);

                    b.HasData(
                        new
                        {
                            TareaId = new Guid("2bf073d5-fb9b-4081-b602-803678782d7c"),
                            CategoriaId = new Guid("3a82ce26-8edd-4c72-9b05-05872b781198"),
                            FechaCreacion = new DateTime(2023, 1, 24, 21, 24, 53, 790, DateTimeKind.Local).AddTicks(1253),
                            PrioridadTarea = "Media",
                            Titulo = "Pago de servicios publicos"
                        },
                        new
                        {
                            TareaId = new Guid("f0de7f3b-50cf-4245-857e-7f13f13012e8"),
                            CategoriaId = new Guid("3a82ce26-8edd-4c72-9b05-05872b781102"),
                            FechaCreacion = new DateTime(2023, 1, 24, 21, 24, 53, 790, DateTimeKind.Local).AddTicks(1266),
                            PrioridadTarea = "Baja",
                            Titulo = "Terminar de ver serie"
                        });
                });

            modelBuilder.Entity("proyectoef.Models.Tarea", b =>
                {
                    b.HasOne("proyectoef.Models.Categoria", "Categoria")
                        .WithMany("Tareas")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("proyectoef.Models.Categoria", b =>
                {
                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
