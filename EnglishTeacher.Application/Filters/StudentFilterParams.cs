using EnglishTeacher.Application.Common;

namespace EnglishTeacher.Application.Filters;

public class StudentFilterParams : PaginationParams
{
    public string? Name { get; set; }

    public int? Age { get; set; }

    public bool IncludeInactive { get; set; } = false;
}