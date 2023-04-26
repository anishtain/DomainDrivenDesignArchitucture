using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;

namespace DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions;
public class BaseException : Exception
{
    public BaseException(ExceptionType type, ExceptionLevel level, string message) : base(message)
    {
        Type = type;
        Level = level;
    }

    public ExceptionType Type { get; private set; }

    public ExceptionLevel Level { get; private set; }
}
