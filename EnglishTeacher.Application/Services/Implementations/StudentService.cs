using AutoMapper;
using EnglishTeacher.Application.Common;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Filters;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<StudentResponseDto>> GetAllAsync(
        StudentFilterParams filter,
        CancellationToken cancellationToken)
    {
        var query = _repository.Query()
            .AsNoTracking();

        // 🔹 filtro ativo/inativo
        if (!filter.IncludeInactive)
            query = query.Where(s => s.IsActive);

        // 🔹 filtro por nome
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(s => s.Name.Contains(filter.Name));

        // 🔹 filtro por idade
        if (filter.Age.HasValue)
            query = query.Where(s => s.Age == filter.Age.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var students = await query
            .OrderBy(s => s.Name)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        var dto = _mapper.Map<List<StudentResponseDto>>(students);

        return new PagedResult<StudentResponseDto>(
            dto,
            totalCount,
            filter.PageNumber,
            filter.PageSize
        );
    }

    public async Task<StudentResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);

        if (student is null)
            return null;

        return _mapper.Map<StudentResponseDto>(student);
    }

    public async Task<StudentResponseDto> CreateAsync(
        StudentCreateDto dto,
        CancellationToken cancellationToken)
    {
        var student = new Student(
            dto.Name,
            dto.Email,
            dto.Age
        );

        await _repository.AddAsync(student, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentResponseDto>(student);
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        StudentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);

        if (student is null)
            return false;

        student.Update(dto.Name, dto.Email, dto.Age);

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);

        if (student is null)
            return false;

        student.Deactivate();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);

        if (student is null)
            return false;

        student.Activate();

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}