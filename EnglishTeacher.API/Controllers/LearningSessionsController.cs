using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningSessionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LearningSessionsController(AppDbContext context)
    {
        _context = context;
    }

    // START SESSION
    // POST: api/learningsessions/start
    [HttpPost("start")]
    public async Task<IActionResult> StartSession([FromBody] StartSessionDto dto)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == dto.StudentId);

        if (student == null)
            return NotFound("Student não encontrado.");

        var lesson = await _context.Lessons
            .Include(l => l.Exercises)
            .FirstOrDefaultAsync(l => l.Id == dto.LessonId);

        if (lesson == null)
            return NotFound("Lesson não encontrada.");

        var totalExercises = lesson.Exercises.Count;

        var session = new LearningSession(
            dto.StudentId,
            dto.LessonId,
            totalExercises
        );

        _context.LearningSessions.Add(session);

        await _context.SaveChangesAsync();

        return Ok(session);
    }

    // ANSWER EXERCISE
    // POST: api/learningsessions/answer
    [HttpPost("answer")]
    public async Task<IActionResult> AnswerExercise([FromBody] AnswerExerciseDto dto)
    {
        var session = await _context.LearningSessions
            .FirstOrDefaultAsync(s => s.Id == dto.SessionId);

        if (session == null)
            return NotFound("Session não encontrada.");

        if (session.FinishedAt != null)
            return BadRequest("Sessão já finalizada.");

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(e => e.Id == dto.ExerciseId);

        if (exercise == null)
            return NotFound("Exercise não encontrado.");

        bool isCorrect = exercise.Answer?.ToLower() == dto.Answer.ToLower();

        session.RegisterAnswer(isCorrect);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            correct = isCorrect,
            correctAnswers = session.CorrectAnswers,
            totalExercises = session.TotalExercises
        });
    }

    // FINISH SESSION
    // POST: api/learningsessions/finish
    [HttpPost("finish")]
    public async Task<IActionResult> FinishSession([FromBody] FinishSessionDto dto)
    {
        var session = await _context.LearningSessions
            .FirstOrDefaultAsync(s => s.Id == dto.SessionId);

        if (session == null)
            return NotFound("Session não encontrada.");

        if (session.FinishedAt != null)
            return BadRequest("Sessão já foi finalizada.");

        session.FinishSession();

        await _context.SaveChangesAsync();

        return Ok(new
        {
            score = session.Score,
            correctAnswers = session.CorrectAnswers,
            totalExercises = session.TotalExercises
        });
    }

    // GET SESSION
    // GET: api/learningsessions/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSession(Guid id)
    {
        var session = await _context.LearningSessions
            .Include(s => s.Student)
            .Include(s => s.Lesson)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            return NotFound();

        return Ok(session);
    }

    // GET ALL SESSIONS FROM STUDENT
    // GET: api/learningsessions/student/{studentId}
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentSessions(Guid studentId)
    {
        var sessions = await _context.LearningSessions
            .Where(s => s.StudentId == studentId)
            .Include(s => s.Lesson)
            .OrderByDescending(s => s.StartedAt)
            .ToListAsync();

        return Ok(sessions);
    }
}


// DTOs

public class StartSessionDto
{
    public Guid StudentId { get; set; }
    public Guid LessonId { get; set; }
}

public class AnswerExerciseDto
{
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public string Answer { get; set; } = null!;
}

public class FinishSessionDto
{
    public Guid SessionId { get; set; }
}