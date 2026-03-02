using EnglishTeacher.Application.DTOs.Auth;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTeacher.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Success = false,
                Errors = new[] { ex.Message }
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                Success = false,
                Errors = new[] { ex.Message }
            });
        }
    }
}