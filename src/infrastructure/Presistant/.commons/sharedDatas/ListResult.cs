namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;

internal sealed record ListResult<T>(int Total, IList<T> List);
