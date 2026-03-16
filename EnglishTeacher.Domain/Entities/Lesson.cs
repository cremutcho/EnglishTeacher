using System;

namespace EnglishTeacher.Domain.Entities;

public class Lesson : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Level { get; private set; }

    public Guid TeacherId { get; private set; }

    public Teacher Teacher { get; private set; } = null!;

    public ICollection<Exercise> Exercises { get; private set; } = new List<Exercise>();

    private Lesson() { }

    public Lesson(string title, string description, string level, Guid teacherId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        if (string.IsNullOrWhiteSpace(level))
            throw new ArgumentException("Level cannot be empty");

        Id = Guid.NewGuid();
        Title = title.Trim();
        Description = description.Trim();
        Level = level.Trim();
        TeacherId = teacherId;
        IsActive = true;
    }

    public void Update(string title, string description, string level)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        Title = title.Trim();
        Description = description.Trim();
        Level = level.Trim();
    }
}