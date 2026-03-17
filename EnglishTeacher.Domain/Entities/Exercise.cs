using EnglishTeacher.Domain.Enums;

namespace EnglishTeacher.Domain.Entities;

public class Exercise : BaseEntity
{
    public Guid LessonId { get; private set; }

    public string Question { get; private set; } = string.Empty;

    public ExerciseType Type { get; private set; }

    public ExerciseDifficulty Difficulty { get; private set; }

    public string? OptionsJson { get; private set; }

    public string? Answer { get; private set; }

    public Lesson Lesson { get; private set; } = null!;

    private Exercise() { }

    public Exercise(
        Guid lessonId,
        string question,
        ExerciseType type,
        ExerciseDifficulty difficulty,
        string? optionsJson,
        string? answer)
    {
        LessonId = lessonId;
        Question = question;
        Type = type;
        Difficulty = difficulty;
        OptionsJson = optionsJson;
        Answer = answer;
    }

    public bool CheckAnswer(string studentAnswer)
    {
        if (string.IsNullOrWhiteSpace(Answer))
            return false;

        return studentAnswer.Trim().ToLower() ==
               Answer.Trim().ToLower();
    }

    public void SetInactive()
    {
        IsActive = false;
    }
}