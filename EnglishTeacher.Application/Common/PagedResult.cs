namespace EnglishTeacher.Application.Common;

public class PagedResult<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }

    public PagedResult(IEnumerable<T> data, int count, int pageNumber, int pageSize)
    {
        Data = data;
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}