using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishTeacher.Domain.Entities;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Question)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Type)
            .IsRequired();

        builder.Property(e => e.OptionsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(e => e.Answer)
            .HasMaxLength(200);

        builder.HasOne(e => e.Lesson)
            .WithMany(l => l.Exercises)
            .HasForeignKey(e => e.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}