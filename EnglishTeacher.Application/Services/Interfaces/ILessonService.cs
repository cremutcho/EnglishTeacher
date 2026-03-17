using EnglishTeacher.Application.DTOs.Lessons;

namespace EnglishTeacher.Application.Services.Interfaces;

public interface ILessonService
{
    Task<LessonResponseDto> CreateAsync(CreateLessonDto dto, CancellationToken cancellationToken);

    Task<IEnumerable<LessonResponseDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<LessonResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}