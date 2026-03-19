using EnglishTeacher.Domain.Enums;

namespace EnglishTeacher.Application.DTOs.Exercises;

public class CreateExerciseDto
{
    public Guid LessonId { get; set; }
    public string Question { get; set; } = string.Empty;
    public ExerciseType Type { get; set; }  // MultipleChoice, FillInTheBlank, Listening
    public ExerciseDifficulty Difficulty { get; set; } // Easy, Medium, Hard
    public string? OptionsJson { get; set; }  // JSON para MultipleChoice ou True/False
    public string? Answer { get; set; }       // Resposta correta
}