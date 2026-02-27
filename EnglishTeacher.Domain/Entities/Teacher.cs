using EnglishTeacher.Domain.Exceptions;

namespace EnglishTeacher.Domain.Entities;

public class Teacher : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Subject { get; private set; } = string.Empty;

    // Construtor protegido para EF Core
    protected Teacher() { }

    public Teacher(string name, string email, string subject)
    {
        SetName(name);
        SetEmail(email);
        SetSubject(subject);
        IsActive = true; // garante que o Teacher é ativo ao criar
    }

    public void Update(string name, string email, string subject)
    {
        if (!IsActive)
            throw new DomainException("Cannot update an inactive teacher.");

        SetName(name);
        SetEmail(email);
        SetSubject(subject);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Teacher name cannot be empty.");

        if (name.Length < 3)
            throw new DomainException("Teacher name must have at least 3 characters.");

        Name = name.Trim();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");

        if (!email.Contains("@"))
            throw new DomainException("Invalid email format.");

        Email = email.Trim().ToLower();
    }

    public void SetSubject(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new DomainException("Subject cannot be empty.");

        Subject = subject.Trim();
    }
}