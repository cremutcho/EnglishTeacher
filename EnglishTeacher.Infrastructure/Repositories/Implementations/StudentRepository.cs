using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Student> Query()
    {
        return _context.Students.AsQueryable();
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken)
    {
        await _context.Students.AddAsync(student, cancellationToken);
    }

    // ✅ NOVO
    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    // ✅ NOVO
    public void Delete(Student student)
    {
        _context.Students.Remove(student);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}