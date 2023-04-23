namespace DomainDrivenDesignArchitucture.Presentation.Api.commons.endpoints;

public sealed record ListResult<T>(IList<T> List, int PageSize, int Page, int Total);
