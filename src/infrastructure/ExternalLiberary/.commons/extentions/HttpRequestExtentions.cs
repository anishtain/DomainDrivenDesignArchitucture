using System.Net;
using System.Text.Json;

namespace DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.extentions;

internal static class HttpRequestExtentions
{
    internal static HttpRequestMessage MakeApiPath<T>(this HttpRequestMessage httpRequestMessage, string path, T data)
    {
        if (data is not null)
        {
            var dataProperties = data.GetType()
           .GetProperties(System.Reflection.BindingFlags.Public)
           .Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(data).ToString()))
           .ToList();
            path += "?";
            foreach (var property in dataProperties)
                path += $"{property.Key}={property.Value}&&";

            path.Remove(path.LastIndexOf("&&"));

        }

        httpRequestMessage.RequestUri = new Uri(path);

        return httpRequestMessage;
    }

    internal static HttpRequestMessage AddHeaders(this HttpRequestMessage httpRequestMessage, params KeyValuePair<string, string>[] headers)
    {
        foreach (var header in headers)
            httpRequestMessage.Headers.Add(header.Key, header.Value);

        return httpRequestMessage;
    }

    internal static HttpRequestMessage AddBody<T>(this HttpRequestMessage httpRequestMessage, T data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        httpRequestMessage.Content = content;
        return httpRequestMessage;
    }

    internal static async Task<HttpResponseMessage> Post(this HttpRequestMessage httpRequestMessage, HttpClient httpClient, CancellationToken cancellationToken)
    {
        httpRequestMessage.Method = HttpMethod.Post;
        var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
        return response;
    }

    internal static async Task<HttpResponseMessage> Get(this HttpRequestMessage httpRequestMessage, HttpClient httpClient, CancellationToken cancellationToken)
    {
        httpRequestMessage.Method = HttpMethod.Get;
        var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
        return response;
    }

    internal static async Task<HttpResponseMessage> On<T>(this Task<HttpResponseMessage> httpResponseMessage, HttpStatusCode statusCode, Action<HttpResponseMessage> act)
    {
        if ((await httpResponseMessage).StatusCode == statusCode)
            act.Invoke(await httpResponseMessage);

        return await httpResponseMessage;
    }
}
