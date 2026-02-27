using AutoMapper;
using EnglishTeacher.Application.DTOs.Teachers;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TeacherService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeacherResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var teachers = await _context.Teachers
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
    }

    public async Task<TeacherResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

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

        await _context.Teachers.AddAsync(teacher, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TeacherResponseDto>(teacher);
    }

    public async Task<bool> UpdateAsync(Guid id, TeacherUpdateDto dto, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (teacher is null)
            return false;

        teacher.Update(
            dto.Name,
            dto.Email,
            dto.Subject
        );

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (teacher is null)
            return false;

        teacher.Deactivate(); // Soft delete

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}