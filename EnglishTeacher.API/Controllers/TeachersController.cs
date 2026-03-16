using EnglishTeacher.Application.DTOs.Teachers;
using EnglishTeacher.Domain.Entities;
using EnglishTeacher.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly AppDbContext _context;

    public TeachersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return Ok(teachers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();
        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = new Teacher(dto.Name, dto.Email, dto.Subject);

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TeacherUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();

        teacher.Update(dto.Name, dto.Email, dto.Subject);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null) return NotFound();

        teacher.Deactivate(); // Soft delete
        await _context.SaveChangesAsync();

        return NoContent();
    }
}