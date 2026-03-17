using System;

namespace EnglishTeacher.Domain.Entities;

public class LearningSession : BaseEntity
{
    public Guid StudentId { get; private set; }

    public Guid LessonId { get; private set; }

    public DateTime StartedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public int TotalExercises { get; private set; }

    public int CorrectAnswers { get; private set; }

    public double Score { get; private set; }

    public Student Student { get; private set; } = null!;

    public Lesson Lesson { get; private set; } = null!;

    private LearningSession() { } // EF Core

    public LearningSession(Guid studentId, Guid lessonId, int totalExercises)
    {
        Id = Guid.NewGuid();

        StudentId = studentId;
        LessonId = lessonId;

        TotalExercises = totalExercises;

        CorrectAnswers = 0;

        StartedAt = DateTime.UtcNow;

        IsActive = true;
    }

    public void RegisterAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            CorrectAnswers++;
        }
    }

    public void FinishSession()
    {
        FinishedAt = DateTime.UtcNow;

        Score = TotalExercises == 0
            ? 0
            : (double)CorrectAnswers / TotalExercises * 100;
    }
}