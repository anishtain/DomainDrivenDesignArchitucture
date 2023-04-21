using DomainDrivenDesignArchitucture.Domain.Contracts.repositories;

namespace DomainDrivenDesignArchitucture.Presentation.Api.extensions;

internal static class Migrator
{
    internal static async Task<WebApplication> Migrate(this WebApplication app)
    {
        using var scop = app.Services.CreateScope();
        using var userManager = scop.ServiceProvider.GetService<IUserStore>();
        using var roleManager = scop.ServiceProvider.GetService<IRoleStore>();

        var adminRoleIsExists = await roleManager.IsRoleWithSpecificNameExists("admin");

        if (!adminRoleIsExists)
            await roleManager.Create("admin", "مدیر سیستم");

        var adminUserIsExists = await userManager.IsUserWithSpecificRoleExists("admin");

        if (!adminUserIsExists)
            await userManager.RegisterUser("admin", "(admin123)", "admin");

        return app;
    }
}
