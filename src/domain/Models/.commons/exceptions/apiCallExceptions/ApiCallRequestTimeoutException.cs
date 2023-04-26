using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions.apiCallExceptions;

public class ApiCallRequestTimeoutException : BaseException
{
    public ApiCallRequestTimeoutException(ExceptionType type, ExceptionLevel level, string message) : base(type, level, message)
    {
    }
}
