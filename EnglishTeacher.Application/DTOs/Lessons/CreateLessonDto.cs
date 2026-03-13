namespace EnglishTeacher.Application.DTOs.Lessons;

public class CreateLessonDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Level { get; set; }

    public Guid TeacherId { get; set; }
}