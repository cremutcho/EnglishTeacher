using System;

namespace EnglishTeacher.Domain.Entities;

public class StudentProgress : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid LessonId { get; private set; }

    public int CompletedExercises { get; private set; }
    public int TotalExercises { get; private set; }

    public double ProgressPercentage { get; private set; }

    public string Status { get; private set; } = "InProgress";

    public double? Score { get; private set; }

    public DateTime LastUpdated { get; private set; }

    public Student Student { get; private set; } = null!;
    public Lesson Lesson { get; private set; } = null!;

    private StudentProgress() { } // EF Core

    public StudentProgress(Guid studentId, Guid lessonId, int totalExercises)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        LessonId = lessonId;
        TotalExercises = totalExercises;
        CompletedExercises = 0;
        ProgressPercentage = 0;
        Status = "InProgress";
        LastUpdated = DateTime.UtcNow;
        IsActive = true;
    }

    public void RegisterExerciseCompletion()
    {
        CompletedExercises++;

        ProgressPercentage = (double)CompletedExercises / TotalExercises * 100;

        LastUpdated = DateTime.UtcNow;

        if (CompletedExercises >= TotalExercises)
        {
            Status = "Completed";
        }
    }

    public void SetScore(double score)
    {
        Score = score;
        LastUpdated = DateTime.UtcNow;
    }
}