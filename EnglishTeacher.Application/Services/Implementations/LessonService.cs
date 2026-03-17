using AutoMapper;
using EnglishTeacher.Application.DTOs.Lessons;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repository;
    private readonly IMapper _mapper;

    public LessonService(ILessonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LessonResponseDto> CreateAsync(CreateLessonDto dto, CancellationToken cancellationToken)
    {
        var lesson = new Lesson(dto.Title, dto.Description, dto.Level, dto.TeacherId);

        await _repository.AddAsync(lesson, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<LessonResponseDto>(lesson);
    }

    public async Task<IEnumerable<LessonResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var lessons = await _repository.Query()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<LessonResponseDto>>(lessons);
    }

    public async Task<LessonResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var lesson = await _repository.GetByIdAsync(id, cancellationToken);
        if (lesson == null) return null;

        return _mapper.Map<LessonResponseDto>(lesson);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateLessonDto dto, CancellationToken cancellationToken)
    {
        var lesson = await _repository.GetByIdAsync(id, cancellationToken);
        if (lesson == null) return false;

        lesson.Update(dto.Title, dto.Description, dto.Level);

        await _repository.UpdateAsync(lesson, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var lesson = await _repository.GetByIdAsync(id, cancellationToken);
        if (lesson == null) return false;

        lesson.Deactivate();

        await _repository.UpdateAsync(lesson, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}