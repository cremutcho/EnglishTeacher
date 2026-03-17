using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<StudentProgress> StudentProgresses { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    public DbSet<LearningSession> LearningSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<Student>()
            .HasQueryFilter(s => s.IsActive);

        builder.Entity<StudentProgress>()
            .HasQueryFilter(sp => sp.IsActive);

        builder.Entity<Teacher>()
            .HasQueryFilter(t => t.IsActive);
    }
}