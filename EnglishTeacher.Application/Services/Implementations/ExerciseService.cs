using AutoMapper;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Application.DTOs.Exercises;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Domain.Enums;

namespace EnglishTeacher.Application.Services.Implementations;

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
            dto.Type,
            dto.Difficulty,
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

        var dto = _mapper.Map<ExerciseResponseDto>(exercise);
        dto.Type = exercise.Type.ToString();

        return dto;
    }

    public async Task<IEnumerable<ExerciseResponseDto>> GetByLessonAsync(Guid lessonId)
    {
        var exercises = await _context.Exercises
            .Where(x => x.LessonId == lessonId)
            .ToListAsync();

        var dtos = exercises.Select(e => new ExerciseResponseDto
        {
            Id = e.Id,
            LessonId = e.LessonId,
            Question = e.Question,
            Type = e.Type.ToString(),
            Difficulty = e.Difficulty.ToString(),
            OptionsJson = e.OptionsJson,
            Answer = e.Answer
        });

        return dtos;
    }

    public async Task DeleteAsync(Guid id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null) return;

        exercise.SetInactive();
        await _context.SaveChangesAsync();
    }

    // ✅ Submeter resposta do aluno (CORRIGIDO)
    public async Task<bool> SubmitAnswerAsync(Guid exerciseId, Guid studentId, string studentAnswer)
    {
        var exercise = await _context.Exercises.FindAsync(exerciseId);
        if (exercise == null)
            throw new Exception("Exercício não encontrado");

        // Corrige automaticamente
        bool isCorrect = exercise.CheckAnswer(studentAnswer);

        // ✅ CORREÇÃO AQUI
        var studentAnswerEntity = new StudentAnswer(
            studentId,
            exerciseId,
            studentAnswer
        );

        studentAnswerEntity.SetCorrection(isCorrect);

        _context.StudentAnswers.Add(studentAnswerEntity);
        await _context.SaveChangesAsync();

        return isCorrect;
    }
}