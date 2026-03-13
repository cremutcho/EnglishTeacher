using EnglishTeacher.Application.DTOs.Lessons;
using EnglishTeacher.Application.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class LessonService : ILessonService
{
    private readonly AppDbContext _context;

    public LessonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LessonResponseDto> CreateAsync(CreateLessonDto dto)
    {
        var lesson = new Lesson(
            dto.Title,
            dto.Description,
            dto.Level,
            dto.TeacherId
        );

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return new LessonResponseDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            Level = lesson.Level,
            TeacherId = lesson.TeacherId
        };
    }

    public async Task<IEnumerable<LessonResponseDto>> GetAllAsync()
    {
        return await _context.Lessons
            .Select(l => new LessonResponseDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                Level = l.Level,
                TeacherId = l.TeacherId
            })
            .ToListAsync();
    }

    public async Task<LessonResponseDto?> GetByIdAsync(Guid id)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
            return null;

        return new LessonResponseDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            Level = lesson.Level,
            TeacherId = lesson.TeacherId
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
            return false;

        lesson.Update(
            dto.Title,
            dto.Description,
            dto.Level
        );

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
            return false;

        lesson.Deactivate();

        await _context.SaveChangesAsync();

        return true;
    }
}