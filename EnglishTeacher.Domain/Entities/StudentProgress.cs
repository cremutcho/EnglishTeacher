using System;

namespace EnglishTeacher.Domain.Entities;

public class StudentProgress : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid LessonId { get; private set; }
    public DateTime CompletedAt { get; private set; }
    public string? Status { get; private set; }
    public double? Score { get; private set; }

    public Student Student { get; private set; } = null!;
    public Lesson Lesson { get; private set; } = null!;

    private StudentProgress() { } // EF Core precisa

    public StudentProgress(Guid studentId, Guid lessonId)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        LessonId = lessonId;
        CompletedAt = DateTime.UtcNow;
        IsActive = true;
    }

    // Novo método para atualizar status e score
    public void UpdateProgress(string status, double? score)
    {
        Status = status;
        Score = score;
        CompletedAt = DateTime.UtcNow; // atualiza a data da conclusão
    }
}