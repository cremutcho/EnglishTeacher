using EnglishTeacher.Domain.Entities;

public class Exercise : BaseEntity
{
    public Guid LessonId { get; private set; }

    public string Question { get; private set; } = string.Empty;

    public ExerciseType Type { get; private set; }

    public string? OptionsJson { get; private set; }

    public string? Answer { get; private set; }

    public Lesson Lesson { get; private set; } = null!;

    private Exercise() { }

    public Exercise(Guid lessonId, string question, ExerciseType type, string? optionsJson, string? answer)
    {
        LessonId = lessonId;
        Question = question;
        Type = type;
        OptionsJson = optionsJson;
        Answer = answer;
    }
    public void SetInactive()
    {
        IsActive = false;
    }

}