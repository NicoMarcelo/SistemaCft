using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaCft.Models;

public partial class SistemacftContext : DbContext
{
    public SistemacftContext()
    {
    }

    public SistemacftContext(DbContextOptions<SistemacftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Asignaturaasignada> Asignaturaasignada { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Asignaturaasignada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturaasignada");

            entity.HasIndex(e => e.AsignaturaId, "fk_Asignatura_has_Estudiante_Asignatura_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_Asignatura_has_Estudiante_Estudiante1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId)
                .HasColumnType("int(11)")
                .HasColumnName("Asignatura_Id");
            entity.Property(e => e.EstudianteId)
                .HasColumnType("int(11)")
                .HasColumnName("Estudiante_Id");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Asignaturaasignada)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Asignatura_has_Estudiante_Asignatura");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Asignaturaasignada)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Asignatura_has_Estudiante_Estudiante1");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Edad).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Rut).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
