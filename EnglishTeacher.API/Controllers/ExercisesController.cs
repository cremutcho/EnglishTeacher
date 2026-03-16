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

    [HttpPost]
    public async Task<IActionResult> Create(CreateExerciseDto dto)
    {
        var exercise = await _service.CreateAsync(dto);
        return Ok(exercise);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var exercise = await _service.GetByIdAsync(id);

        if (exercise == null)
            return NotFound();

        return Ok(exercise);
    }

    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetByLesson(Guid lessonId)
    {
        var exercises = await _service.GetByLessonAsync(lessonId);
        return Ok(exercises);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}