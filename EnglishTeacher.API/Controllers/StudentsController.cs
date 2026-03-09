using EnglishTeacher.Application.Common;
using EnglishTeacher.Application.DTOs.Students;
using EnglishTeacher.Application.Services.Interfaces;
using EnglishTeacher.Application.Filters;
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

    // GET: api/students?pageNumber=1&pageSize=10&name=Lucas&includeInactive=false
    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentResponseDto>>> GetAll(
        [FromQuery] StudentFilterParams filter,
        CancellationToken cancellationToken)
    {
        var pagination = new PaginationParams
        {
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };

        var result = await _service.GetAllAsync(
            pagination,
            filter,
            filter.IncludeInactive,
            cancellationToken);

        return Ok(result);
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
            return NotFound(new { message = "Aluno não encontrado ou está inativo." });

        return NoContent();
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deactivated = await _service.DeactivateAsync(id, cancellationToken);

        if (!deactivated)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }

    // PATCH: api/students/{id}/activate
    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var activated = await _service.ActivateAsync(id, cancellationToken);

        if (!activated)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }
}