using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Infrastrcturcture.Repositories;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}
