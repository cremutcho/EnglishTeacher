namespace EnglishTeacher.Application.Filters;

public class StudentFilterParams
{
    public string? Name { get; set; }

    public int? Age { get; set; }

    public bool IncludeInactive { get; set; } = false;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 50 ? 50 : value;
    }
}