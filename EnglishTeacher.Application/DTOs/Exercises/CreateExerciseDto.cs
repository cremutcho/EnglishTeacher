namespace EnglishTeacher.Application.DTOs.Exercises;

public class CreateExerciseDto
{
    public Guid LessonId { get; set; }

    public string Question { get; set; } = string.Empty;

    public int Type { get; set; }

    public string? OptionsJson { get; set; }

    public string? Answer { get; set; }
}