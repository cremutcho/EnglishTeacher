using System.Net;

namespace EnglishTeacher.Application.Common.Exceptions;

public class BusinessRuleException : BaseException
{
    public BusinessRuleException(string message)
        : base(message, (int)HttpStatusCode.UnprocessableEntity)
    {
    }
}