using Fiever.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fiever.Infrastructure.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One-to-One between Student and Passport
        modelBuilder.Entity<Passport>()
            .HasOne(p => p.Student)
            .WithOne(s => s.Passport)
            .HasForeignKey<Passport>(p => p.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-Many between University and Students
        modelBuilder.Entity<University>()
            .HasMany(u => u.Students)
            .WithOne(s => s.University)
            .HasForeignKey(s => s.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-Many between Students and Courses
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
    }
}