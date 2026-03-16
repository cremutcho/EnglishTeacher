using EnglishTeacher.Application.DTOs.Lessons;
using EnglishTeacher.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLessonDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var lesson = await _lessonService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = lesson.Id }, lesson);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lessons = await _lessonService.GetAllAsync();
        return Ok(lessons);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var lesson = await _lessonService.GetByIdAsync(id);

        if (lesson == null)
            return NotFound();

        return Ok(lesson);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLessonDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _lessonService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _lessonService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}