using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.Infrastructure.Repositories.Interfaces;

public interface IStudentRepository
{
    // 🔎 Query base (para filtros, paginação, etc)
    IQueryable<Student> Query();

    // 🔍 Busca por ID
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // ➕ Criar
    Task AddAsync(Student student, CancellationToken cancellationToken);

    // 🔄 Atualizar
    void Update(Student student);

    // ❌ Remover (caso precise futuramente)
    void Delete(Student student);

    // 💾 Persistência
    Task SaveChangesAsync(CancellationToken cancellationToken);
}