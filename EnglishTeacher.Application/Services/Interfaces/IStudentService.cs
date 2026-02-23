using EnglishTeacher.Application.DTOs.Students;

namespace EnglishTeacher.Application.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDto>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<StudentResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<StudentResponseDto> CreateAsync(
        StudentCreateDto dto,
        CancellationToken cancellationToken);

    Task<bool> UpdateAsync(
        Guid id,
        StudentUpdateDto dto,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);
}