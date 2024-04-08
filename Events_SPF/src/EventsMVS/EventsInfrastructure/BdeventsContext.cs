using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EventsDomain.Models;

namespace EventsInfrastructure;

public partial class BdeventsContext : DbContext
{
    public BdeventsContext()
    {
    }

    public BdeventsContext(DbContextOptions<BdeventsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EducationProgram> EducationPrograms { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventParticipation> EventParticipations { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentParliamentMember> StudentParliamentMembers { get; set; }

    public virtual DbSet<StudentParliamentPosition> StudentParliamentPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=localhost;Database=BDEvents;User Id=sa;Password=StrongPassword123!@#;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Departments_PK");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);

            entity.HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Departments_Faculties");
        });

        modelBuilder.Entity<EducationProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EducationPrograms_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.EducationPrograms)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EducationPrograms_Faculties_FK");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Events_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<EventParticipation>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.EventId }).HasName("EventParticipation_PK");

            entity.ToTable("EventParticipation");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.Result)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.EventParticipations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventParticipation_Events_FK");

            entity.HasOne(d => d.Student).WithMany(p => p.EventParticipations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EventParticipation_Students_FK");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Faculties_PK");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Students_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Students_Departments_FK");
        });

        modelBuilder.Entity<StudentParliamentMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StudentParliamentMembers_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Position).WithMany(p => p.StudentParliamentMembers)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StudentParliamentMembers_SPositions_FK");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentParliamentMembers)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StudentParliamentMembers_Students_FK");
        });

        modelBuilder.Entity<StudentParliamentPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StudentParliamentPositions_PK");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
