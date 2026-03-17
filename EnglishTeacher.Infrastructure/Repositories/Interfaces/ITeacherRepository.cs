using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Repositories.Interfaces;

public interface ITeacherRepository
{
    IQueryable<Teacher> Query();

    Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Teacher teacher, CancellationToken cancellationToken);

    void Update(Teacher teacher);

    void Delete(Teacher teacher);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}