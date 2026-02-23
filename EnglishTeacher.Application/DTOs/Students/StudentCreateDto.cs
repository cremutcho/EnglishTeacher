namespace EnglishTeacher.Application.DTOs.Students;

public class StudentCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
}