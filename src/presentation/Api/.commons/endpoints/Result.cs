namespace DomainDrivenDesignArchitucture.Presentation.Api.commons.endpoints;

public sealed record Result<T>(int Code, bool IsSuccess, T Data, Error Error = null);
