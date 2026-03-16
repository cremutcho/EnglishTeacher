using EnglishTeacher.Application.DTOs.StudentAnswers;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class StudentAnswerService : IStudentAnswerService
{
    private readonly AppDbContext _context;

    public StudentAnswerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentAnswerResponseDto> SubmitAnswerAsync(CreateStudentAnswerDto dto)
    {
        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(x => x.Id == dto.ExerciseId);

        if (exercise == null)
            throw new Exception("Exercise not found");

        // Comparar resposta do aluno com a resposta correta
        var isCorrect =
            exercise.Answer?.Trim().ToLower() ==
            dto.Answer.Trim().ToLower();

        var studentAnswer = new StudentAnswer(
            dto.StudentId,
            dto.ExerciseId,
            dto.Answer,
            isCorrect
        );

        _context.StudentAnswers.Add(studentAnswer);

        await _context.SaveChangesAsync();

        return new StudentAnswerResponseDto
        {
            Id = studentAnswer.Id,
            StudentId = studentAnswer.StudentId,
            ExerciseId = studentAnswer.ExerciseId,
            Answer = studentAnswer.Answer,
            IsCorrect = studentAnswer.IsCorrect
        };
    }
}