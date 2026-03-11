using System.Net;

namespace EnglishTeacher.Application.Common.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException(string message)
        : base(message, (int)HttpStatusCode.BadRequest)
    {
    }
}