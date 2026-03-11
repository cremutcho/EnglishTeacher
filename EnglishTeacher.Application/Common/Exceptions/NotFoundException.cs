using System.Net;

namespace EnglishTeacher.Application.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, (int)HttpStatusCode.NotFound)
    {
    }
}