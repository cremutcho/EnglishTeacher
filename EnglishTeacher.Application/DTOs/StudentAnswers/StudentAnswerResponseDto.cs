namespace EnglishTeacher.Application.DTOs.StudentAnswers;

public class StudentAnswerResponseDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ExerciseId { get; set; }
    public string Answer { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}