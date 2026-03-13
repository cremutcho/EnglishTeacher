using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Domain.Entities;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentProgressController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/StudentProgress
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentProgress>>> GetAll()
    {
        return await _context.StudentProgresses
            .Include(sp => sp.Student)
            .Include(sp => sp.Lesson)
                .ThenInclude(l => l.Teacher)
            .ToListAsync();
    }

    // GET: api/StudentProgress/student/{studentId}
    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<StudentProgress>>> GetByStudent(Guid studentId)
    {
        var progress = await _context.StudentProgresses
            .Include(sp => sp.Student)
            .Include(sp => sp.Lesson)
                .ThenInclude(l => l.Teacher)
            .Where(sp => sp.StudentId == studentId)
            .ToListAsync();

        return Ok(progress);
    }

    // GET: api/StudentProgress/lesson/{lessonId}
    [HttpGet("lesson/{lessonId}")]
    public async Task<ActionResult<IEnumerable<StudentProgress>>> GetByLesson(Guid lessonId)
    {
        var progress = await _context.StudentProgresses
            .Include(sp => sp.Student)
            .Include(sp => sp.Lesson)
                .ThenInclude(l => l.Teacher)
            .Where(sp => sp.LessonId == lessonId)
            .ToListAsync();

        return Ok(progress);
    }

    // POST: api/StudentProgress
    [HttpPost]
    public async Task<ActionResult<StudentProgress>> Create([FromBody] CreateStudentProgressDto dto)
    {
        var student = await _context.Students.FindAsync(dto.StudentId);
        var lesson = await _context.Lessons.FindAsync(dto.LessonId);

        if (student == null || lesson == null)
            return BadRequest("Aluno ou Lesson inválido.");

        // Evita progresso duplicado
        var exists = await _context.StudentProgresses
            .AnyAsync(sp => sp.StudentId == dto.StudentId && sp.LessonId == dto.LessonId);

        if (exists)
            return BadRequest("Este aluno já possui progresso registrado para esta aula.");

        var progress = new StudentProgress(dto.StudentId, dto.LessonId);

        _context.StudentProgresses.Add(progress);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = progress.Id }, progress);
    }

    // PUT: api/StudentProgress/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateStudentProgressDto dto)
    {
        var progress = await _context.StudentProgresses.FindAsync(id);

        if (progress == null)
            return NotFound("Progresso não encontrado.");

        progress.UpdateProgress(dto.Status, dto.Score);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}


// DTOs

public class CreateStudentProgressDto
{
    public Guid StudentId { get; set; }
    public Guid LessonId { get; set; }
}

public class UpdateStudentProgressDto
{
    public string Status { get; set; } = null!;
    public double? Score { get; set; }
}