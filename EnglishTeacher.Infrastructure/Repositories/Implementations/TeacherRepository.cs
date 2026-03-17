using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Teacher> Query()
    {
        return _context.Teachers.AsQueryable();
    }

    public async Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken)
    {
        await _context.Teachers.AddAsync(teacher, cancellationToken);
    }

    public void Update(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
    }

    public void Delete(Teacher teacher)
    {
        _context.Teachers.Remove(teacher);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}