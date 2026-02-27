using EnglishTeacher.Application.DTOs.Teachers;

namespace EnglishTeacher.Application.Services.Interfaces;

public interface ITeacherService
{
    Task<IEnumerable<TeacherResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TeacherResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TeacherResponseDto> CreateAsync(TeacherCreateDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, TeacherUpdateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}