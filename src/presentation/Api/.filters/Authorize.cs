using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomainDrivenDesignArchitucture.Presentation.Api.filters;
public class Authorize : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public Authorize(string permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userService = (IUserStore)context.HttpContext.RequestServices.GetService(typeof(IUserStore));
        var roleService = (IRoleStore)context.HttpContext.RequestServices.GetService(typeof(IRoleStore));

        var currentUser = await userService.GetCurrentUserId();

        if (currentUser == null)
            throw new Exception(" برای دسترسی به این سرویس نیاز به احراز هویت است.");

        var allowedPermissions = await roleService.GetCurrentPermissions();

        if (allowedPermissions == null || allowedPermissions.Count() == 0 || !allowedPermissions.Any(x => x == _permission))
            throw new Exception("شما اجازه دسترسی به این سرور را ندارید.");
    }
}
