using EnglishTeacher.Application.DTOs.Lessons;

namespace EnglishTeacher.Application.Interfaces;

public interface ILessonService
{
    Task<LessonResponseDto> CreateAsync(CreateLessonDto dto);

    Task<IEnumerable<LessonResponseDto>> GetAllAsync();

    Task<LessonResponseDto?> GetByIdAsync(Guid id);

    Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto);

    Task<bool> DeleteAsync(Guid id);
}