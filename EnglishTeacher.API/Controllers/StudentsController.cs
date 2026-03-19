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

    // 🔹 Listar todos com filtros e paginação
    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentResponseDto>>> GetAll(
        [FromQuery] StudentFilterParams filter,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(filter, cancellationToken);
        return Ok(result);
    }

    // 🔹 Buscar por ID
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

    // 🔹 Criar aluno
    [HttpPost]
    public async Task<ActionResult<StudentResponseDto>> Create(
        [FromBody] StudentCreateDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // 🔹 Atualizar aluno
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

    // 🔹 Desativar aluno (SOFT DELETE)
    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _service.DeactivateAsync(id, cancellationToken);

        if (!result)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }

    // 🔹 Ativar aluno
    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _service.ActivateAsync(id, cancellationToken);

        if (!result)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }

    // 🔹 Remover definitivamente (opcional - HARD DELETE)
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _service.DeactivateAsync(id, cancellationToken);

        if (!result)
            return NotFound(new { message = "Aluno não encontrado." });

        return NoContent();
    }

    // 🔹 Ranking de alunos
    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetRanking()
    {
        var ranking = await _service.GetRankingAsync();
        return Ok(ranking);
    }
}