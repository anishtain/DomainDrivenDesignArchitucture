using DomainDrivenDesignArchitucture.Presentation.Api.commons.endpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomainDrivenDesignArchitucture.Presentation.Api.filters;

public sealed class Validation : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var validationErrorList = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value.Errors.Select(err => err.ErrorMessage));
            var persianErrorMessages = validationErrorList.SelectMany(x => x.Value).Where(x => !x.ToLower().Contains("value"));
            if (persianErrorMessages.Any())
            {
                string message = persianErrorMessages.First();
                context.Result = new OkObjectResult(new Result<dynamic>(400, false, null, new Error(message)));
                return;
            }
        }

        await next();
    }
}
