using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions.apiCallExceptions;

public sealed class ApiCallUnauthorizedException : BaseException
{
    public ApiCallUnauthorizedException(ExceptionType type, ExceptionLevel level, string message) : base(type, level, message)
    {
    }
}
