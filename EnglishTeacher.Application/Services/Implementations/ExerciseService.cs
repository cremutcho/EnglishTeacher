using AutoMapper;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Application.DTOs.Exercises;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Domain.Enums;

public class ExerciseService : IExerciseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExerciseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExerciseResponseDto> CreateAsync(CreateExerciseDto dto)
    {
        var exercise = new Exercise(
            dto.LessonId,
            dto.Question,
            (ExerciseType)dto.Type,
            (ExerciseDifficulty)dto.Difficulty,
            dto.OptionsJson,
            dto.Answer
        );

        _context.Exercises.Add(exercise);

        await _context.SaveChangesAsync();

        return _mapper.Map<ExerciseResponseDto>(exercise);
    }

    public async Task<ExerciseResponseDto?> GetByIdAsync(Guid id)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(x => x.Id == id);

        if (exercise == null) return null;

        return _mapper.Map<ExerciseResponseDto>(exercise);
    }

    public async Task<IEnumerable<ExerciseResponseDto>> GetByLessonAsync(Guid lessonId)
    {
        var exercises = await _context.Exercises
            .Where(x => x.LessonId == lessonId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ExerciseResponseDto>>(exercises);
    }

    public async Task DeleteAsync(Guid id)
    {
        var exercise = await _context.Exercises.FindAsync(id);

        if (exercise == null) return;

        exercise.SetInactive();

        await _context.SaveChangesAsync();
    }
}