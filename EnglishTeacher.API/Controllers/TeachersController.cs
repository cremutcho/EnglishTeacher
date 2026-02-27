using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnglishTeacher.Infrastructure.Data;
using EnglishTeacher.Domain.Entities;

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

    // GET: api/teachers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return Ok(teachers);
    }

    // GET: api/teachers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
            return NotFound();
        return Ok(teacher);
    }

    // POST: api/teachers
    [HttpPost]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }

    // PUT: api/teachers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Teacher updatedTeacher)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
            return NotFound();

        // ✅ Usando método Update da entidade
        teacher.Update(updatedTeacher.Name, updatedTeacher.Email, updatedTeacher.Subject);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/teachers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
            return NotFound();

        teacher.Deactivate(); // Soft delete

        await _context.SaveChangesAsync();
        return NoContent();
    }
}