using AutoMapper;
using EnglishTeacher.Application.Common;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Filters;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<StudentResponseDto>> GetAllAsync(
        PaginationParams pagination,
        StudentFilterParams filter,
        bool includeInactive,
        CancellationToken cancellationToken)
    {
        var query = _context.Students
            .AsNoTracking()
            .AsQueryable();

        // 🔹 filtro ativo/inativo
        if (!includeInactive)
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
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        var dto = _mapper.Map<List<StudentResponseDto>>(students);

        return new PagedResult<StudentResponseDto>(
            dto,
            totalCount,
            pagination.PageNumber,
            pagination.PageSize
        );
    }

    public async Task<StudentResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

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

        await _context.Students.AddAsync(student, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentResponseDto>(student);
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        StudentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
            return false;

        student.Update(dto.Name, dto.Email, dto.Age);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
            return false;

        student.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
            return false;

        student.Activate();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}