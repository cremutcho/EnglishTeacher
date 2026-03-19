using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<StudentProgress> StudentProgresses { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<StudentAnswer> StudentAnswers { get; set; } = null!;
    public DbSet<LearningSession> LearningSessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Aplica todas as configurações automaticamente
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Query filters para dados ativos
        builder.Entity<Student>().HasQueryFilter(s => s.IsActive);
        builder.Entity<StudentProgress>().HasQueryFilter(sp => sp.IsActive);
        builder.Entity<Teacher>().HasQueryFilter(t => t.IsActive);
        builder.Entity<Exercise>().HasQueryFilter(e => e.IsActive);
    }
}