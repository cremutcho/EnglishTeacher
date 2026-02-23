using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTeacher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var students = await _service.GetAllAsync(cancellationToken);
        return Ok(students);
    }

    // GET: api/students/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudentResponseDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var student = await _service.GetByIdAsync(id, cancellationToken);

        if (student is null)
            return NotFound(new { message = "Aluno não encontrado." });

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create(
        [FromBody] StudentCreateDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT: api/students/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] StudentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto, cancellationToken);

        if (!updated)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(id, cancellationToken);

        if (!deleted)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }
}