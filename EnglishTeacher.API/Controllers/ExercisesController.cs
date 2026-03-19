using Microsoft.AspNetCore.Mvc;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Application.DTOs.Exercises;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _service;

    public ExercisesController(IExerciseService service)
    {
        _service = service;
    }

    // POST: api/Exercises
    [HttpPost]
    public async Task<IActionResult> Create(CreateExerciseDto dto)
    {
        var exercise = await _service.CreateAsync(dto);
        return Ok(exercise);
    }

    // GET: api/Exercises/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var exercise = await _service.GetByIdAsync(id);
        if (exercise == null) return NotFound();
        return Ok(exercise);
    }

    // GET: api/Exercises/lesson/{lessonId}
    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetByLesson(Guid lessonId)
    {
        var exercises = await _service.GetByLessonAsync(lessonId);
        return Ok(exercises);
    }

    // DELETE: api/Exercises/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // POST: api/Exercises/{id}/SubmitAnswer
    [HttpPost("{id}/SubmitAnswer")]
    public async Task<IActionResult> SubmitAnswer(Guid id, [FromQuery] Guid studentId, [FromBody] string studentAnswer)
    {
        try
        {
            var isCorrect = await _service.SubmitAnswerAsync(id, studentId, studentAnswer);
            return Ok(new { Correct = isCorrect });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}