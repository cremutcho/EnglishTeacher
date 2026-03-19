using EnglishTeacher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
{
    public void Configure(EntityTypeBuilder<StudentAnswer> builder)
    {
        builder.ToTable("StudentAnswers");

        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.Answer)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(sa => sa.IsCorrect)
            .IsRequired();

        builder.HasOne(sa => sa.Student)
            .WithMany()
            .HasForeignKey(sa => sa.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sa => sa.Exercise)
            .WithMany()
            .HasForeignKey(sa => sa.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}