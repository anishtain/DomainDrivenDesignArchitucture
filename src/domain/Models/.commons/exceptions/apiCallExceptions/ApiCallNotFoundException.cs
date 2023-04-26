using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions.apiCallExceptions;
public class ApiCallNotFoundException : BaseException
{
    public ApiCallNotFoundException(ExceptionType type, ExceptionLevel level, string message) : base(type, level, message)
    {
    }
}
