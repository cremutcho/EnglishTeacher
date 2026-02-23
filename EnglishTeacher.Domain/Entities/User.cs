namespace EnglishTeacher.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    // ⚠️ V1: pode ser senha em texto ou hash simples
    // V2+: substituir por hash com BCrypt
    public string PasswordHash { get; set; } = string.Empty;

    // Ex: "User", "Admin"
    public string Role { get; set; } = "User";

    // Auditoria básica (boa prática)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}