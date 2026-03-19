using EnglishTeacher.Domain.Enums;

namespace EnglishTeacher.Domain.Entities;

public class Exercise : BaseEntity
{
    public Guid LessonId { get; private set; }
    public string Question { get; private set; } = string.Empty;
    public ExerciseType Type { get; private set; }
    public ExerciseDifficulty Difficulty { get; private set; }
    public string? OptionsJson { get; private set; } // JSON para MultipleChoice ou TrueFalse
    public string? Answer { get; private set; } // Resposta correta
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
        IsActive = true; // Inicializa como ativo
    }

    /// <summary>
    /// Corrige a resposta do aluno e retorna true/false.
    /// </summary>
    public bool CheckAnswer(string studentAnswer)
    {
        if (string.IsNullOrWhiteSpace(Answer))
            return false;

        switch (Type)
        {
            case ExerciseType.MultipleChoice:
            case ExerciseType.FillInTheBlank:
            case ExerciseType.TrueFalse:
            case ExerciseType.Listening:
                return studentAnswer.Trim().ToLower() == Answer.Trim().ToLower();
            default:
                return false;
        }
    }

    /// <summary>
    /// Marca o exercício como inativo.
    /// </summary>
    public void SetInactive()
    {
        IsActive = false;
    }

    public int GetPoints()
{
    return Difficulty switch
    {
        ExerciseDifficulty.Easy => 10,
        ExerciseDifficulty.Medium => 20,
        ExerciseDifficulty.Hard => 30,
        _ => 0
    };
}
}