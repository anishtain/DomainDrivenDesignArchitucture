using DomainDrivenDesignArchitucture.Domain.Models.commons.exceptions;
using DomainDrivenDesignArchitucture.Presentation.Api.commons.endpoints;
using System.Text;
using System.Text.Json;

namespace DomainDrivenDesignArchitucture.Presentation.Api.middlewares;

public class GlobalExceptionMiddelware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddelware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        var responseBuffer = context.Response.Body;
        context.Response.Body = new MemoryStream();
        try
        {
            await _next.Invoke(context);
        }
        catch (BaseException exc) when (exc.Type != ExceptionType.Validation)
        {
            var errorResponse = new Result<dynamic>((int)exc.Type, false, null, new Error(exc.Message));

            var errorResponseJson = JsonSerializer.Serialize(errorResponse);
            var error = new StringContent(errorResponseJson, Encoding.UTF8, "application/json");

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            context.Response.Body.CopyTo(await error.ReadAsStreamAsync());
            context.Response.Body = responseBuffer;
        }
    }
}
