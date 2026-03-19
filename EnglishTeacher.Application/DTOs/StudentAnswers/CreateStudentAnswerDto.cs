namespace EnglishTeacher.Application.DTOs.StudentAnswers;

public class CreateStudentAnswerDto
{
    public Guid StudentId { get; set; }
    public Guid ExerciseId { get; set; }
    public string Answer { get; set; } = string.Empty;
}