namespace DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.bases;
internal interface IRestService
{
    Task<T> Post<T, U, Z>(string serviceName, string path, U param, Z data, CancellationToken cancellationToken, params KeyValuePair<string, string>[] headers);

    Task<T> Get<T, U>(string serviceName, string path, U param, CancellationToken cancellationToken, params KeyValuePair<string, string>[] headers);
}