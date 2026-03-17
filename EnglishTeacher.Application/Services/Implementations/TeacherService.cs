using AutoMapper;
using EnglishTeacher.Application.DTOs.Teachers;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;
    private readonly IMapper _mapper;

    public TeacherService(ITeacherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeacherResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var teachers = await _repository
            .Query()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
    }

    public async Task<TeacherResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id, cancellationToken);

        if (teacher is null)
            return null;

        return _mapper.Map<TeacherResponseDto>(teacher);
    }

    public async Task<TeacherResponseDto> CreateAsync(TeacherCreateDto dto, CancellationToken cancellationToken)
    {
        var teacher = new Teacher(
            dto.Name,
            dto.Email,
            dto.Subject
        );

        await _repository.AddAsync(teacher, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherResponseDto>(teacher);
    }

    public async Task<bool> UpdateAsync(Guid id, TeacherUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id, cancellationToken);

        if (teacher is null)
            return false;

        teacher.Update(
            dto.Name,
            dto.Email,
            dto.Subject
        );

        _repository.Update(teacher);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetByIdAsync(id, cancellationToken);

        if (teacher is null)
            return false;

        teacher.Deactivate(); // Soft delete

        _repository.Update(teacher);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}