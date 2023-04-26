using DomainDrivenDesignArchitucture.Domain.Models.commons.enums;
using DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions;
using DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions.apiCallExceptions;
using DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.extentions;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.bases;

internal class RestApiService : IRestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RestApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<T> Post<T, U, Z>(string serviceName, string path, U param, Z data, CancellationToken cancellationToken, params KeyValuePair<string, string>[] headers)
    {
        var httpClient = _httpClientFactory.CreateClient(serviceName);

        var httpRequestMessage = new HttpRequestMessage();

        T response = default(T);

        await httpRequestMessage
            .MakeApiPath(path, param)
            .AddHeaders(headers)
            .AddBody(data)
            .Post(httpClient, cancellationToken)
            .On<T>(HttpStatusCode.OK,
            async res => response = JsonSerializer.Deserialize<T>(await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.BadRequest,
            async res => throw new ApiCallBadRequestException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.Unauthorized,
            async res => throw new ApiCallUnauthorizedException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.NotFound,
            async res => throw new ApiCallNotFoundException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.RequestTimeout,
            async res => throw new ApiCallRequestTimeoutException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.InternalServerError,
            async res => throw new ApiCallInternalServerErrorException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()));

        return response;
    }

    public async Task<T> Get<T, U>(string serviceName, string path, U param, CancellationToken cancellationToken, params KeyValuePair<string, string>[] headers)
    {
        var httpClient = _httpClientFactory.CreateClient(serviceName);

        var httpRequestMessage = new HttpRequestMessage();

        T response = default(T);

        await httpRequestMessage
            .MakeApiPath(path, param)
            .AddHeaders(headers)
            .Get(httpClient, cancellationToken)
            .On<T>(HttpStatusCode.OK,
            async res => response = JsonSerializer.Deserialize<T>(await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.BadRequest,
            async res => throw new ApiCallBadRequestException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.Unauthorized,
            async res => throw new ApiCallUnauthorizedException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.NotFound,
            async res => throw new ApiCallNotFoundException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.RequestTimeout,
            async res => throw new ApiCallRequestTimeoutException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()))
            .On<T>(HttpStatusCode.InternalServerError,
            async res => throw new ApiCallInternalServerErrorException(ExceptionType.ApiCallFailed, ExceptionLevel.Infrastructure, await res.Content.ReadAsStringAsync()));

        return response;
    }
}
