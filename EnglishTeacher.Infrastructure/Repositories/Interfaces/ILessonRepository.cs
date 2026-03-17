using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Repositories.Interfaces;

public interface ILessonRepository
{
    IQueryable<Lesson> Query();

    Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Lesson lesson, CancellationToken cancellationToken);

    Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}