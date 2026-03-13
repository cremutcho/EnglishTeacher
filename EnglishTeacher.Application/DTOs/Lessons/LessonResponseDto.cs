namespace EnglishTeacher.Application.DTOs.Lessons;

public class LessonResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Level { get; set; }

    public Guid TeacherId { get; set; }
}