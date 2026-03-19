using EnglishTeacher.Application.DTOs.StudentAnswers;

namespace EnglishTeacher.Application.Services.Interfaces;

public interface IStudentAnswerService
{
    Task<StudentAnswerResponseDto> SubmitAnswerAsync(CreateStudentAnswerDto dto);
}