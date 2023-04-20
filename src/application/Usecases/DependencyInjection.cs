using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Application.Usecases;
public static class DependencyInjection
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        return services;
    }
}
