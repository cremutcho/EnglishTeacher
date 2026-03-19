using EnglishTeacher.Domain.Exceptions;

namespace EnglishTeacher.Domain.Entities;

public class Student : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public int Age { get; private set; }

    public int Score { get; private set; } // 🔥 NOVO

    protected Student() { }

    public Student(string name, string email, int age)
    {
        SetName(name);
        SetEmail(email);
        SetAge(age);
        Score = 0;
    }

    public void Update(string name, string email, int age)
    {
        EnsureIsActive();

        SetName(name);
        SetEmail(email);
        SetAge(age);
    }

    public void AddScore(int points) // 🔥 NOVO
    {
        if (points <= 0) return;
        Score += points;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("Student is already inactive.");

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            throw new DomainException("Student is already active.");

        IsActive = true;
    }

    private void EnsureIsActive()
    {
        if (!IsActive)
            throw new DomainException("Cannot perform this operation on an inactive student.");
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Student name cannot be empty.");

        if (name.Length < 3)
            throw new DomainException("Student name must have at least 3 characters.");

        Name = name.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");

        if (!email.Contains("@"))
            throw new DomainException("Invalid email format.");

        Email = email.Trim().ToLower();
    }

    private void SetAge(int age)
    {
        if (age < 5 || age > 100)
            throw new DomainException("Student age must be between 5 and 100.");

        Age = age;
    }
}