using AutoMapper;
using EnglishTeacher.Application.Common;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Filters;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // 🔹 Listar alunos com filtros e paginação
    public async Task<PagedResult<StudentResponseDto>> GetAllAsync(
        StudentFilterParams filter,
        CancellationToken cancellationToken)
    {
        var query = _repository.Query().AsNoTracking();

        if (!filter.IncludeInactive)
            query = query.Where(s => s.IsActive);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(s => s.Name.Contains(filter.Name));

        if (filter.Age.HasValue)
            query = query.Where(s => s.Age == filter.Age.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var students = await query
            .OrderBy(s => s.Name)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        var dto = _mapper.Map<List<StudentResponseDto>>(students);

        return new PagedResult<StudentResponseDto>(
            dto,
            totalCount,
            filter.PageNumber,
            filter.PageSize
        );
    }

    // 🔹 Buscar aluno por ID
    public async Task<StudentResponseDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);
        if (student is null) return null;

        return _mapper.Map<StudentResponseDto>(student);
    }

    // 🔹 Criar aluno
    public async Task<StudentResponseDto> CreateAsync(
        StudentCreateDto dto,
        CancellationToken cancellationToken)
    {
        var student = new Student(dto.Name, dto.Email, dto.Age);

        await _repository.AddAsync(student, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StudentResponseDto>(student);
    }

    // 🔹 Atualizar aluno
    public async Task<bool> UpdateAsync(
        Guid id,
        StudentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);
        if (student is null) return false;

        student.Update(dto.Name, dto.Email, dto.Age);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    // 🔹 Desativar aluno
    public async Task<bool> DeactivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(id, cancellationToken);
        if (student is null) return false;

        student.Deactivate();
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    // 🔹 Ativar aluno (corrigido: busca TODOS os alunos, inclusive desativados)
    public async Task<bool> ActivateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        // 🔹 Buscar aluno ignorando filtros globais (ex: IsActive)
        var student = await _repository.Query()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (student is null)
            return false;

        student.Activate();
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    // 🔹 Ranking de alunos por pontuação
    public async Task<IEnumerable<StudentResponseDto>> GetRankingAsync()
    {
        var students = await _repository.Query()
            .AsNoTracking()
            .OrderByDescending(s => s.Score)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
    }
}