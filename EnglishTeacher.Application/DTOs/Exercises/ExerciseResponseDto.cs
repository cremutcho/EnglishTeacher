namespace EnglishTeacher.Application.DTOs.Exercises;

public class ExerciseResponseDto
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;  // Retorna como string legível

    public string Difficulty { get; set; } = string.Empty;
    public string? OptionsJson { get; set; }          // JSON de opções
    public string? Answer { get; set; }               // Resposta correta
    public bool IsActive { get; set; }               // Se o exercício está ativo
}