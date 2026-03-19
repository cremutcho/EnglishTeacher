using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Domain.Entities;

public class StudentAnswer : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public string Answer { get; private set; } = string.Empty;
    public bool IsCorrect { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Student Student { get; private set; } = null!;
    public Exercise Exercise { get; private set; } = null!;

    private StudentAnswer() { }

    public StudentAnswer(Guid studentId, Guid exerciseId, string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be empty");

        StudentId = studentId;
        ExerciseId = exerciseId;
        Answer = answer;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetCorrection(bool isCorrect)
    {
        IsCorrect = isCorrect;
    }
}