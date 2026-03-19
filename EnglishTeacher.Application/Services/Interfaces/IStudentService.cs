using EnglishTeacher.Application.Common;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Filters;

namespace EnglishTeacher.Application.Services.Interfaces;

public interface IStudentService
{
    Task<PagedResult<StudentResponseDto>> GetAllAsync(
        StudentFilterParams filter,
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

    Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken);

    // 🔥 NOVO: Ranking de alunos por pontuação
    Task<IEnumerable<StudentResponseDto>> GetRankingAsync();
}