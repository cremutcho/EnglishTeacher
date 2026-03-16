using EnglishTeacher.Application.DTOs.StudentAnswers;

public interface IStudentAnswerService
{
    Task<StudentAnswerResponseDto> SubmitAnswerAsync(CreateStudentAnswerDto dto);
}