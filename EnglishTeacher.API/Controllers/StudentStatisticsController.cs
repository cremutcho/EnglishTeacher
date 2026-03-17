using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Infrastructure.Data;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/students")]
public class StudentStatisticsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentStatisticsController(AppDbContext context)
    {
        _context = context;
    }

    // ================================
    // 📊 STUDENT STATISTICS
    // ================================
    // GET: api/students/{id}/statistics
    [HttpGet("{id}/statistics")]
    public async Task<IActionResult> GetStudentStatistics(Guid id)
    {
        var student = await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null)
            return NotFound("Student não encontrado.");

        var sessions = await _context.LearningSessions
            .Where(s => s.StudentId == id && s.FinishedAt != null)
            .AsNoTracking()
            .ToListAsync();

        var progress = await _context.StudentProgresses
            .Where(p => p.StudentId == id)
            .AsNoTracking()
            .ToListAsync();

        var studySessions = sessions.Count;

        var totalExercises = sessions.Sum(s => s.TotalExercises);

        var correctAnswers = sessions.Sum(s => s.CorrectAnswers);

        var lessonsCompleted = progress.Count(p => p.Status == "Completed");

        var accuracy = totalExercises == 0
            ? 0
            : (double)correctAnswers / totalExercises * 100;

        return Ok(new
        {
            student = student.Name,
            studySessions,
            lessonsCompleted,
            totalExercises,
            correctAnswers,
            accuracy = Math.Round(accuracy, 2)
        });
    }

    // ================================
    // 🥇 LEADERBOARD
    // ================================
    // GET: api/students/leaderboard
    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var leaderboard = await _context.LearningSessions
            .Where(s => s.FinishedAt != null)
            .GroupBy(s => s.StudentId)
            .Select(g => new
            {
                StudentId = g.Key,
                TotalScore = g.Sum(s => s.Score),
                TotalCorrectAnswers = g.Sum(s => s.CorrectAnswers)
            })
            .OrderByDescending(x => x.TotalScore)
            .Take(10)
            .Join(_context.Students,
                  session => session.StudentId,
                  student => student.Id,
                  (session, student) => new
                  {
                      student = student.Name,
                      score = session.TotalScore,
                      correctAnswers = session.TotalCorrectAnswers
                  })
            .AsNoTracking()
            .ToListAsync();

        return Ok(leaderboard);
    }

    // ================================
    // 📚 LEARNING HISTORY
    // ================================
    // GET: api/students/{id}/learning-history
    [HttpGet("{id}/learning-history")]
    public async Task<IActionResult> GetLearningHistory(Guid id)
    {
        var history = await _context.LearningSessions
            .Where(s => s.StudentId == id && s.FinishedAt != null)
            .Include(s => s.Lesson)
            .OrderByDescending(s => s.FinishedAt)
            .Select(s => new
            {
                lesson = s.Lesson.Title,
                score = s.Score,
                correctAnswers = s.CorrectAnswers,
                totalExercises = s.TotalExercises,
                date = s.FinishedAt
            })
            .AsNoTracking()
            .ToListAsync();

        return Ok(history);
    }
}