using DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.bases;
using DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary.commons.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceSources = configuration.GetSection("Services").Get<List<RestApiConfig>>();

        foreach (var serviceSource in serviceSources)
            services.AddHttpClient(serviceSource.ServiceName, cfg => cfg.BaseAddress = new Uri(serviceSource.BaseDomain));

        services.AddScoped<IRestService, RestApiService>();

        return services;
    }
}
