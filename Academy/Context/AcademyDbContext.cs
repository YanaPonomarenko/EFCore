using Academy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Context;

public class AcademyDbContext:DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Curator> Curators { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Grade> Grades { get; set; }

    public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options) { }
    public AcademyDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            DbContextConfigurator.Configure(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Description).IsRequired(false);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(g => g.Value).IsRequired();
        });


        modelBuilder.Entity<Group>()
            .HasOne(g => g.Curator)
            .WithOne(c => c.Group)
            .HasForeignKey<Curator>(c => c.GroupId);


        modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId);


        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SubjectId);
    }
}

    //public DbSet<Teacher> Teachers { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //base.OnConfiguring(optionsBuilder);
    //optionsBuilder.UseSqlServer("Server=QQ12\\SQLEXPRESS;Database=academyPV521;Trusted_Connection=True;TrustServerCertificate=True;");
    //}

