using EnglishTeacher.Domain.Exceptions;

namespace EnglishTeacher.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool IsActive { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        IsActive = true;
    }

    public void Activate()
    {
        if (IsActive)
            throw new DomainException($"{GetType().Name} is already active.");

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException($"{GetType().Name} is already inactive.");

        IsActive = false;
    }
}