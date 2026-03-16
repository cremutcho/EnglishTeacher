using EnglishTeacher.Application.DTOs.StudentAnswers;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentAnswersController : ControllerBase
{
    private readonly IStudentAnswerService _service;

    public StudentAnswersController(IStudentAnswerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(CreateStudentAnswerDto dto)
    {
        var result = await _service.SubmitAnswerAsync(dto);

        return Ok(result);
    }
}