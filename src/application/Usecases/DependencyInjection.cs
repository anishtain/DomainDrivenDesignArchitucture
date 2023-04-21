using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Application.Usecases;
public static class DependencyInjection
{
    public static IServiceCollection AddUseCase(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(AssemblyReferences.ASSEMBLY_TYPE));

        services.AddAutoMapper(AssemblyReferences.ASSEMBLY_TYPE);

        services.AddValidatorsFromAssembly(AssemblyReferences.ASSEMBLY);

        return services;
    }
}
