using DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant;

public static class DependencyInjection
{
    public static IServiceCollection AddPresistante(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }
}
