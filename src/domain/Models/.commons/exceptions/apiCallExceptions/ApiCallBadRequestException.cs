using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions.apiCallExceptions;

public sealed class ApiCallBadRequestException : BaseException
{
    public ApiCallBadRequestException(ExceptionType type, ExceptionLevel level, string message) : base(type, level, message)
    {
    }
}
