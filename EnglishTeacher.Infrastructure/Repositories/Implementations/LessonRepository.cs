using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Infrastructure.Repositories.Implementations;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _context;

    public LessonRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Lesson> Query()
    {
        return _context.Lessons.AsQueryable();
    }

    public async Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task AddAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        await _context.Lessons.AddAsync(lesson, cancellationToken);
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        _context.Lessons.Update(lesson);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}