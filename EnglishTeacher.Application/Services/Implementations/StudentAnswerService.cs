using EnglishTeacher.Application.DTOs.StudentAnswers;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.Application.Services.Implementations;

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

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == dto.StudentId);

        if (student == null)
            throw new Exception("Student not found");

        var isCorrect = exercise.CheckAnswer(dto.Answer);

        var studentAnswer = new StudentAnswer(
            dto.StudentId,
            dto.ExerciseId,
            dto.Answer
        );

        studentAnswer.SetCorrection(isCorrect);

        // 🔥 XP
        if (isCorrect)
        {
            var points = exercise.GetPoints();
            student.AddScore(points);
        }

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