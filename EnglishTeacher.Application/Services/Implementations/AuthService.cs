using EnglishTeacher.Application.DTOs.Auth;
using EnglishTeacher.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishTeacher.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    // ===============================
    // REGISTER
    // ===============================
    public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null)
            throw new Exception("Usuário já existe.");

        var user = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        // 🔐 ROLE FIXA (não vem do usuário)
        var role = "Teacher";

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);

        return await GenerateJwtTokenAsync(user, role);
    }

    // ===============================
    // LOGIN
    // ===============================
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!validPassword)
            throw new Exception("Senha inválida.");

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Teacher"; // default se algo deu errado

        return await GenerateJwtTokenAsync(user, role);
    }

    // ===============================
    // TOKEN GENERATION
    // ===============================
    private async Task<LoginResponseDto> GenerateJwtTokenAsync(IdentityUser user, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        claims.Add(new Claim(ClaimTypes.Role, role));

        var jwtKey = _configuration["Jwt:Key"]!;
        var jwtIssuer = _configuration["Jwt:Issuer"]!;
        var jwtAudience = _configuration["Jwt:Audience"]!;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            Token = tokenString,
            Expiration = expiration,
            Email = user.Email!,
            Role = role
        };
    }
}