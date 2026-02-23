using AutoMapper;
using EnglishTeacher.Application.DTOs.Students;
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

    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var students = await _context.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
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
        var student = _mapper.Map<Student>(dto);
        student.Id = Guid.NewGuid();

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
            .FindAsync([id], cancellationToken);

        if (student is null)
            return false;

        _mapper.Map(dto, student);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FindAsync([id], cancellationToken);

        if (student is null)
            return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}